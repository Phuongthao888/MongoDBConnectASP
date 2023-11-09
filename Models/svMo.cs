using System.Drawing.Printing;

namespace Mong.Models
{
    public class svMo
    {
        private string id;
        private string name;
        private string address1;
        private string gmail1;
        private int sdt1;

        public string Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string address { get => address1; set => address1 = value; }
        public string gmail { get => gmail1; set => gmail1 = value; }
        public int sdt { get => sdt1; set => sdt1 = value; }
    }
}
