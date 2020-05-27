namespace Hahn.ApplicatonProcess.May2020.Web.ApiRespource.Request
{
    public class ApplicantRequest
    {
        /// <summary>
        /// Applicant's name
        /// </summary>
        /// <example>Timilehin</example>
        public string name { get; set; }

        /// <summary>
        /// Applicant's lastname
        /// </summary>
        /// <example>Ogunseye</example>
        public string familyName { get; set; }

        /// <summary>
        /// Applicant's address
        /// </summary>
        /// <example> Lorem ipsum, twenty five kilomters, Jesus Street, Munich</example>
        public string address { get; set; }

        /// <summary>
        /// Applicant's country of origin
        /// </summary>
        /// <example> Germany</example>
        public string countryOfOrigin { get; set; }

        /// <summary>
        /// Applicant's email
        /// </summary>
        ///  <example>ogunseye.timilehin@gmail.com</example>
        public string emailAddress { get; set; }

        /// <summary>
        /// Applicant age
        /// </summary>
        /// <example> 28 </example>
        public int age { get; set; }

        /// <summary>
        /// Is applicant hired?
        /// </summary>
        ///  <example> true </example>
        public bool? hired { get; set; } = false;
    }
}