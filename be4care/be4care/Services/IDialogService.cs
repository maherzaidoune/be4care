using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace be4care.Services
{
    interface IDialogService
    {
        void ShowMessage(string message, bool error);
        void verifier();
    }
}
