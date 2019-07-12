using BackOffice.Models.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackOffice.Utility
{
    public class ImageUtility
    {
        public static string GetCastingImage(ActionType? actionType)
        {
            switch (actionType)
            {
                case ActionType.Normal:
                    return "~/images/Action_NoBackground.png";
                case ActionType.Reaction:
                    return "~/images/Reaction_NoBackground.png";
                case ActionType.Free:
                    return "~/images/FreeAction_NoBackground.png";
                case ActionType.Long:
                default:
                    return "";
            }
        }
    }
}
