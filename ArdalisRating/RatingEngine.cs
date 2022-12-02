using ArdalisRating.Logging;
using ArdalisRating.Persistence;
using ArdalisRating.PolicyRating;
using ArdalisRating.Serialization;

namespace ArdalisRating
{
    /// <summary>
    /// The RatingEngine reads the policy application details from a file and produces a numeric 
    /// rating value based on the details.
    /// </summary>
    public class RatingEngine
    {
        public PolicySerializer PolicySerializer { get; set; } = new PolicySerializer();
        public FilePolicySource PolicySource { get; set; } = new FilePolicySource();
        public ConsoleLogger Logger { get; set; } = new ConsoleLogger();
        public decimal Rating { get; set; }
        public void Rate()
        {
            Logger.Log("Starting rate.");

            Logger.Log("Loading policy.");

            // load policy - open file policy.json
            string policyJson = PolicySource.GetPolicyFromSource();

            var policy = PolicySerializer.GetPolicyFromJsonString(policyJson);

            switch (policy.Type)
            {
                case PolicyType.Auto:
                    var autoRater = new AutoPolicyRater(this, this.Logger);
                    autoRater.Rate(policy);
                    break;

                case PolicyType.Land:
                    var landRater = new LandPolicyRater(this, this.Logger);
                    landRater.Rate(policy);
                    break;

                case PolicyType.Life:
                    var lifeRater = new LifePolicyRater(this, this.Logger);
                    lifeRater.Rate(policy);
                    break;

                default:
                    Logger.Log("Unknown policy type");
                    break;
            }

            Logger.Log("Rating completed.");
        }
    }
}
