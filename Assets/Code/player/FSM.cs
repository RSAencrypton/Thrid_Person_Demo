namespace player {
    public abstract class FSM
    {
        protected playerController _obj;

        public abstract void enterState(playerController obj);
        public abstract void Action();
        public abstract void exitState();


    }
}

