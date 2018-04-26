using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace be4care.Services
{
    interface IDialogService
    {
        void ShowMessage(string message, string title, string buttonText,bool hasButton);
    }
}
