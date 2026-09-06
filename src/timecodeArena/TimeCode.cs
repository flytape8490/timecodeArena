using System;
using System.Linq;

namespace timecodeArena;

// todo: summary "Represents an unsigned number of frames."
internal class TimeCode
{
    internal int Hours { get; private set; }
    internal int Minutes { get; private set; }
    internal int Seconds { get; private set; }
    internal int Frames { get; private set; }

    internal int TotalFrames { get; }

    internal TimeCode(int frames)
        : this(0, 0, 0, frames) {}

    internal TimeCode(int seconds, int frames)
        : this(0, 0, seconds, frames) {}

    internal TimeCode(int minutes, int seconds, int frames)
        : this(0, minutes, seconds, frames) {}

    internal TimeCode() : this(0) {}

    internal TimeCode(int hours, int minutes, int seconds, int frames)
    {
        Hours = hours;
        Minutes = minutes;
        Seconds = seconds;
        Frames = frames;

        Normalize(this);

        minutes += hours * 60;
        seconds += minutes * 60;
        frames += seconds * Program.FPS;
        TotalFrames = frames;
    }

    internal static void Normalize(TimeCode timeCode)
    {
        while (timeCode.Frames >= Program.FPS)
        {
            timeCode.Frames -= Program.FPS;
            timeCode.Seconds += 1;
        }

        while (timeCode.Seconds >= 60)
        {
            timeCode.Seconds -= 60;
            timeCode.Minutes += 1;
        }

        while (timeCode.Minutes >= 60)
        {
            timeCode.Minutes -= 60;
            timeCode.Hours += 1;
        }
    }

    internal static bool TryParse(string s, out TimeCode timeCode, char delimiter = ':')
    {
        timeCode = default;

        string[] string_array = s.Split(delimiter);
        if (string_array.Any(v => int.TryParse(v, out _) == false))
        {
            return false;
        }

        int[] stamp_array = string_array.Select(v => int.Parse(v))
                                        .ToArray();
                             

        if (stamp_array.Count() == 4)
        {
            timeCode = new TimeCode(stamp_array[0]
                                  , stamp_array[1]
                                  , stamp_array[2]
                                  , stamp_array[3]
                                  );
        }

        else if (stamp_array.Count() == 3)
        {
            timeCode = new TimeCode(stamp_array[0]
                                  , stamp_array[1]
                                  , stamp_array[2]
                                  );
        }

        else if (stamp_array.Count() == 2)
        {
            timeCode = new TimeCode(stamp_array[0]
                                  , stamp_array[1]
                                  );
        }

        else if (stamp_array.Count() == 1)
        {
            timeCode = new TimeCode(stamp_array[0]);
        }

        else
        {
            return false;
        }

        return true;
    }


    public override string ToString()
        => ToString(':');

    internal string ToString(char delimiter)
        => $"{Hours:00}{delimiter}{Minutes:00}{delimiter}{Seconds:00}{delimiter}{Frames:00}";
}