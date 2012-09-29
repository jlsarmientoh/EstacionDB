using System;
using System.Collections.Generic;
using System.Text;

namespace EstacionDB.DTO
{
    public class WorkStatusDTO
    {
        public const int START = 1;
        public const int IN_PROGRESS = 2;
        public const int END = 3;

        private int status;

        public int Status
        {
            get { return status; }
            set { status = value; }
        }

        private int percentage;

        public int Percentage
        {
            get { return percentage; }
            set { percentage = value; }
        }
        private string message;

        public string Message
        {
            get { return message; }
            set { message = value; }
        }
    }
}
