using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace Quiz
{
    public class QuizHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello();


        }

        public void GetReady()
        {
            // Call the addNewMessageToPage method to update clients.
            Clients.All.getReady();
        }
    }
}