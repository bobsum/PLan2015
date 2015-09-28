namespace Bismuth.Framework.Animations.Timelines
{
    public enum FillBehavior
    {
        HoldEnd,
        Repeat,
        ReverseAndRepeat
    }

    public interface ITimeline
    {
        float BeginTime { get; set; }
        float EndTime { get; set; }
        float Duration { get; set; }

        FillBehavior FillBehavior { get; set; }

        void Update(float time);
    }
}
