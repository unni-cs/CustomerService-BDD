using System.Text.Json;

namespace SpecFlowProject1.StepDefinitions
{
    [Binding]
    public class CustomerAPIStepDefinitions
    {

        HttpClient client = new HttpClient();
        HttpRequestMessage message = new HttpRequestMessage();
        Models.Customer? customer;
        private string baseUri = "http://localhost:8080/api/customer/";
        HttpResponseMessage? response;


        [Given(@"I have a valid customer '([^']*)'")]
        public void GivenIHaveAValidCustomerName(string name)
        {
            message.Method = HttpMethod.Get;
            message.RequestUri = new Uri(baseUri + name);
        }

        [Given(@"I have a valid token for authentication")]
        public void GivenIHaveAValidTokenForAuthentication()
        {
            client.DefaultRequestHeaders.Add("Authorization", "Bearer 246faa4a-6c8e-430e-92a2-545551f67bfb");
        }

       

        [Given(@"I have customer '([^']*)' that does not exist")]
        public void GivenIHaveCustomerThatDoesNotExist(string name)
        {
            message.Method = HttpMethod.Get;
            message.RequestUri = new Uri(baseUri + name);
        }

        [When(@"I call the customer api")]
        public void WhenICallTheCustomerApi()
        {
            response = client.Send(message);
            string responseBody = response.Content.ReadAsStringAsync().Result;
            customer = string.IsNullOrEmpty(responseBody) ? null : JsonSerializer.Deserialize<Models.Customer>(responseBody);
        }


        [Then(@"i should get customer name as '([^']*)' and credit details as (.*)")]
        public void ThenIShouldGetCustomerNameAsJayAndCreditDetailsAs(string name ,int credit)
        {
            Assert.NotNull(customer);
            Assert.Equal(name, customer?.name);
            Assert.Equal(credit, customer?.credit);
        }

        [Then(@"I Should get customer not found (.*) status")]
        public void ThenIShouldGetCustomerNotFoundStatus(int status)
        {
            Assert.Equal(status, (int)response.StatusCode);
            Assert.Null(customer);
        }


    }
}
