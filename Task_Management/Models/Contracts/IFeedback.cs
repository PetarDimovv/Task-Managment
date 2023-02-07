using System;
using System.Collections.Generic;
using System.Text;
using Task_Management.Models.StatusOfFeedBack;

namespace Task_Management.Models.Contracts
{
    public interface IFeedback : ITask
    {
        int Rating { get; }
        StatusFeedback Status { get; }
        void ChangeFeedbackStatus(StatusFeedback status);
        void ChangeFeedbackRating(int rating);

    }
}
