namespace CommandPattern
{
    public class ReverseAim : Command
    {
        private readonly GameController _controller;

        public ReverseAim(GameController controller)
        {
            _controller = controller;
        }

        public override void Execute()
        {
            _controller.aimModifier *= -1;
        }
    }
}
