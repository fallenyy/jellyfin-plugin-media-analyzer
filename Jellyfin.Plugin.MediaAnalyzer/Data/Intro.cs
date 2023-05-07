using System;
using System.Text.Json.Serialization;

namespace Jellyfin.Plugin.MediaAnalyzer;

/// <summary>
/// Result of fingerprinting and analyzing two episodes in a season.
/// All times are measured in seconds relative to the beginning of the media file.
/// </summary>
public class Intro
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Intro"/> class.
    /// </summary>
    /// <param name="episode">Episode.</param>
    /// <param name="intro">Introduction time range.</param>
    public Intro(Guid episode, TimeRange intro)
    {
        EpisodeId = episode;
        IntroStart = intro.Start;
        IntroEnd = intro.End;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Intro"/> class.
    /// </summary>
    /// <param name="episode">Episode.</param>
    public Intro(Guid episode)
    {
        EpisodeId = episode;
        IntroStart = 0;
        IntroEnd = 0;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Intro"/> class.
    /// </summary>
    /// <param name="intro">intro.</param>
    public Intro(Intro intro)
    {
        EpisodeId = intro.EpisodeId;
        IntroStart = intro.IntroStart;
        IntroEnd = intro.IntroEnd;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Intro"/> class.
    /// </summary>
    public Intro()
    {
    }

    /// <summary>
    /// Gets or sets the Episode ID.
    /// </summary>
    public Guid EpisodeId { get; set; }

    /// <summary>
    /// Gets a value indicating whether this introduction is valid or not.
    /// Invalid results must not be returned through the API.
    /// </summary>
    public bool Valid => IntroEnd > 0;

    /// <summary>
    /// Gets the duration of this intro.
    /// </summary>
    [JsonIgnore]
    public double Duration => IntroEnd - IntroStart;

    /// <summary>
    /// Gets or sets the introduction sequence start time.
    /// </summary>
    public double IntroStart { get; set; }

    /// <summary>
    /// Gets or sets the introduction sequence end time.
    /// </summary>
    public double IntroEnd { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the Into was from db.
    /// </summary>
    public bool FromDB { get; set; }
}

/// <summary>
/// An Intro class with episode metadata. Only used in end to end testing programs.
/// </summary>
public class IntroWithMetadata : Intro
{
    /// <summary>
    /// Initializes a new instance of the <see cref="IntroWithMetadata"/> class.
    /// </summary>
    /// <param name="series">Series name.</param>
    /// <param name="season">Season number.</param>
    /// <param name="title">Episode title.</param>
    /// <param name="intro">Intro timestamps.</param>
    public IntroWithMetadata(string series, int season, string title, Intro intro)
    {
        Series = series;
        Season = season;
        Title = title;

        EpisodeId = intro.EpisodeId;
        IntroStart = intro.IntroStart;
        IntroEnd = intro.IntroEnd;
    }

    /// <summary>
    /// Gets or sets the series name of the TV episode associated with this intro.
    /// </summary>
    public string Series { get; set; }

    /// <summary>
    /// Gets or sets the season number of the TV episode associated with this intro.
    /// </summary>
    public int Season { get; set; }

    /// <summary>
    /// Gets or sets the title of the TV episode associated with this intro.
    /// </summary>
    public string Title { get; set; }
}
