using ENTITIES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DalActivity : IDisposable
    {
        private DbPrCrmEntities _context;
        public DalActivity()
        {
            _context = new DbPrCrmEntities();
        }
        void IDisposable.Dispose()
        {
        }

        public CrmResponse InsertNewActivity(ENTITIES.Activity activity)
        {
            CrmResponse response = new CrmResponse();
            try
            {
                Activity newActivity = new Activity();
                Type myType = newActivity.GetType();
                PropertyInfo[] props = myType.GetProperties();
                foreach (PropertyInfo prop in props)
                {
                    object temp = activity.GetType().GetProperty(prop.Name);
                    if (!(temp is null))
                    {
                        object activityValue = activity.GetType().GetProperty(prop.Name).GetValue(activity);
                        newActivity.GetType().GetProperty(prop.Name).SetValue(newActivity, activityValue);
                    }
                }

                //buttons
                newActivity.Button1 = activity.Buttons[0];
                newActivity.Button2 = activity.Buttons[1];
                newActivity.Button3 = activity.Buttons[2];
                newActivity.InsertDate = DateTime.Now;

                Activity act = _context.Activities.Add(newActivity);
                _context.SaveChanges();
                int activityId = act.ActivityId;

                //textBoxes
                List<TextBox> textBoxes = new List<TextBox>();
                foreach (var item in activity.TextBoxList)
                {
                    TextBox textBox = new TextBox
                    {
                        ActivityId = activityId,
                        Bold = item.Bold,
                        Color = item.Color,
                        FontSize = item.FontSize,
                        Text = item.Text,
                        TextBoxId = item.BoxId
                    };
                    textBoxes.Add(textBox);
                }
                _context.TextBoxes.AddRange(textBoxes);
                _context.SaveChanges();

                //JBI
                List<ActivityJBI> JBIs = new List<ActivityJBI>();
                foreach (var item in activity.JBIlist)
                {
                    ActivityJBI jbi = new ActivityJBI
                    {
                        ActivityId = activityId,
                        JBIId = item.Moneln
                    };
                    JBIs.Add(jbi);
                }
                _context.ActivityJBIs.AddRange(JBIs);
                _context.SaveChanges();
                response.rc = activityId;

            }
            catch (Exception ex)
            {
                var message = ex.Message;
                response.desc = ex.Message;
                response.rc = -1;
            }

            return response;


        }

        public TemplateListResponse GetTemplates()
        {
            TemplateListResponse response = new TemplateListResponse();
            try
            {
                List<Template> templates = _context.Templates.ToList();
                foreach (Template item in templates)
                {
                    ENTITIES.Template newTemplate = new ENTITIES.Template();
                    Copy(ref newTemplate, item);
                    response.Templates.Add(newTemplate);
                }
            }
            catch (Exception ex)
            {
                response.desc = ex.Message;
                response.rc = -1;
            }
            return response;
        }

        public ActivitiesListResponse GetActivitiesList(ActivityFilters filters)
        {
            ActivitiesListResponse response = new ActivitiesListResponse();

            try
            {
                List<Activity> activitiesList = _context.Activities.ToList();
                if (filters.company != 0 || filters.fromDate != "" || filters.toDate != "")
                {
                    if (filters.company != 0)
                        activitiesList = activitiesList.Where((x) => x.Company == filters.company).ToList();
                    if (filters.fromDate != "")
                        activitiesList = activitiesList.Where((x) => x.InsertDate >= Convert.ToDateTime(filters.fromDate)).ToList();
                    if (filters.toDate != "")
                        activitiesList = activitiesList.Where((x) => x.InsertDate <= Convert.ToDateTime(filters.toDate)).ToList();

                }
                foreach (Activity item in activitiesList)
                {
                    ENTITIES.Activity newActivity = new ENTITIES.Activity();
                    Copy(ref newActivity, item);

                    List<ENTITIES.JBI> activitiesJBI = new List<ENTITIES.JBI>();

                    List<ActivityJBI> JBIlist = _context.ActivityJBIs.Where(x => x.ActivityId == item.ActivityId).ToList();
                    foreach (ActivityJBI jbi in JBIlist)
                    {
                        JBI onePerson = _context.JBIs.Where(x => x.Moneln == jbi.JBIId).FirstOrDefault();
                        ENTITIES.JBI oneJBI = new ENTITIES.JBI();
                        Copy(ref oneJBI, onePerson);
                        activitiesJBI.Add(oneJBI);
                    }

                    List<ENTITIES.TextBox> activitiesTextBoxes = new List<ENTITIES.TextBox>();
                    List<TextBox> textBoxes = _context.TextBoxes.Where(x => x.ActivityId == item.ActivityId).ToList();
                    foreach (TextBox textBox in textBoxes)
                    {
                        ENTITIES.TextBox oneTextBox = new ENTITIES.TextBox();
                        Copy(ref oneTextBox, textBox);
                        oneTextBox.BoxId = textBox.TextBoxId;
                        activitiesTextBoxes.Add(oneTextBox);

                    }
                    newActivity.JBIlist = activitiesJBI.ToArray();
                    newActivity.TextBoxList = activitiesTextBoxes.ToArray();
                    response.Activities.Add(newActivity);
                }

            }
            catch (Exception ex)
            {
                response.desc = ex.Message;
                response.rc = -1;
            }
            return response;

        }

        public CrmResponse ScheduleEmails(int activityId)
        {
            CrmResponse response = new CrmResponse();
            try
            {
                Activity activity = _context.Activities.Where((x) => x.ActivityId == activityId).FirstOrDefault();
                if (activity is null) throw new Exception("Activity is not exist in Activity table");
                bool isScheduleActivity = activity.SendingType == 0 ? false : true;
                List<EmailsQueue> RowsToInsert = new List<EmailsQueue>();
                List<ActivityJBI> JBIs = _context.ActivityJBIs.Where((x) => x.ActivityId == activityId).ToList();
                if (JBIs is null) throw new Exception("JBI list of activity: " + activityId + " is null");
                DateTime now = DateTime.Now; //take snap of time now

                if (isScheduleActivity)
                {
                    int periodBetweenSending = (int)activity.PeriodBetweenSending;
                    foreach (ActivityJBI item in JBIs)
                    {
                        for (int i = 0; i < activity.MessagesPerPerson; i++)
                        {
                            EmailsQueue row = new EmailsQueue
                            {
                                ActivityId = activityId,
                                ActivityJBI = item.JBIId,
                                SendDt = now,
                                Done = false
                            };
                            RowsToInsert.Add(row);

                            //add time to now
                            switch (activity.PeriodBetweenSendingType)
                            {
                                case 0: //minutes
                                    now = now.AddMinutes(periodBetweenSending);
                                    break;
                                case 1: //hours
                                    now = now.AddHours(periodBetweenSending);
                                    break;
                                case 2: //days
                                    now = now.AddDays(periodBetweenSending);
                                    break;
                                case 3: //weeks
                                    now = now.AddDays(periodBetweenSending * 7);
                                    break;
                            }
                        }
                    }
                }
                else
                {


                    foreach (ActivityJBI item in JBIs)
                    {
                        EmailsQueue row = new EmailsQueue
                        {
                            ActivityId = activityId,
                            ActivityJBI = item.JBIId,
                            SendDt = now,
                            Done = false
                        };

                        RowsToInsert.Add(row);
                    }
                }
                _context.EmailsQueues.AddRange(RowsToInsert);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                response.desc = ex.Message;
                response.rc = -1;
            }
            return response;

        }

        static void Copy<T, V>(ref T newObject, V item)
        {
            Type myType = newObject.GetType();
            PropertyInfo[] props = myType.GetProperties();
            foreach (PropertyInfo prop in props)
            {
                object temp = item.GetType().GetProperty(prop.Name);
                if (!(temp is null))
                {
                    object activityValue = item.GetType().GetProperty(prop.Name).GetValue(item);
                    newObject.GetType().GetProperty(prop.Name).SetValue(newObject, activityValue);
                }
            }
        }
    }

}
