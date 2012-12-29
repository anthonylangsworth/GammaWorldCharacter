using System;
using System.Collections.Generic;

namespace GammaWorldCharacter.Powers
{
    /// <summary>
    /// Details about sustaining a power.
    /// </summary>
    public class SustainDetails
    {
        private ActionType action;
        private string text;

        /// <summary>
        /// Create a new <see cref="SustainDetails"/>.
        /// </summary>
        /// <param name="action">
        /// The action required to sustain a power.
        /// </param>
        /// <param name="text">
        /// Description of the effect sustaining the power causes.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Action must be Ninor, Move or Standard.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// text cannot be null.
        /// </exception>
        public SustainDetails(ActionType action, string text)
        {
            if (action == ActionType.None
                || action == ActionType.ImmediateInterrupt
                || action == ActionType.ImmediateReaction)
            {
                throw new ArgumentException("Action must be minor, move or standard", "action");
            }
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentNullException("text");
            }

            this.action = action;
            this.text = text;
        }

        /// <summary>
        /// The action required to sustain the power.
        /// </summary>
        public ActionType Action
        {
            get
            {
                return action;
            }
        }

        /// <summary>
        /// Description of the effect of sustaining the power.
        /// </summary>
        public string Text
        {
            get
            {
                return text;
            }
        }
    }
}
