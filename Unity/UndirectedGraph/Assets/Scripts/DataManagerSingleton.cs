namespace GraphContent
{
///
///Singleton Class which Saves the Data
///

    public class DataManagerSingleton
    {
        private static DataManagerSingleton _instance = null;
        private static readonly object Padlock = new object();

        private bool _menuState = false;

        public bool MenuState
        {
            get { return _menuState; }
            set { _menuState = value; }
        }

        private DataManagerSingleton()
        {
        }

        public static DataManagerSingleton Instance
        {
            get
            {
                lock (Padlock)
                {
                    if (_instance == null)
                    {
                        _instance = new DataManagerSingleton();
                    }

                    return _instance;
                }
            }
        }
    }
}