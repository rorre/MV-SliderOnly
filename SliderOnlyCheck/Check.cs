using MapsetParser.objects;
using MapsetParser.objects.hitobjects;
using MapsetParser.objects.timinglines;
using MapsetParser.statics;
using MapsetVerifierFramework;
using MapsetVerifierFramework.objects;
using MapsetVerifierFramework.objects.metadata;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;

namespace SliderOnlyCheck
{
    public class CheckSliderOnly : BeatmapCheck
    {
        public override CheckMetadata GetMetadata() => new BeatmapCheckMetadata()
        {   
            Modes = new Beatmap.Mode[]
            {
                Beatmap.Mode.Standard
            },
            Difficulties = new Beatmap.Difficulty[]
            {
                Beatmap.Difficulty.Easy,
                Beatmap.Difficulty.Normal
            },
            Category = "Compose",
            Message = "Slider only section.",
            Author = "-Keitaro",

            Documentation = new Dictionary<string, string>()
            {
                {
                    "Purpose",
                    @"
                    Preventing slider only section in easy and normal difficulties."
                },
                {
                    "Reasoning",
                    @"
                    Having a slider only section in lower difficulty without proper time gap
                    and/or circles could be aim tiring for new players."
                }
            }
        };
        
        public override Dictionary<string, IssueTemplate> GetTemplates()
        {
            return new Dictionary<string, IssueTemplate>()
            {
                { "Warning",
                    new IssueTemplate(Issue.Level.Warning,
                        "{0} Slider-only section more than 5s. Please ensure time gap is fine. ({1}s long)",
                        "timestamp - ", "duration")
                    .WithCause(
                        "Slider-only section could only be allowed as long as the time gap between sliders makes sense" +
                        " to make some aim rest moments.") }
            };
        }

        public override IEnumerable<Issue> GetIssues(Beatmap aBeatmap)
        {
            var duration = 0.0;
            var last_tail = 0.0;
            HitObject first_object = default(HitObject);

            foreach (HitObject hitObject in aBeatmap.hitObjects)
            {
                if (hitObject is Slider) {
                    if (first_object == default(HitObject)) {
                        first_object = hitObject;
                    }

                    if (last_tail != 0) {
                        duration += hitObject.time - last_tail;
                    }

                    duration += hitObject.GetEndTime() - hitObject.time;
                    last_tail = hitObject.GetEndTime();
                } else {
                    if (duration > 5000.0) {
                        yield return new Issue(GetTemplate("Warning"), aBeatmap, Timestamp.Get(first_object), $"{(int)duration/1000}");
                    }
                    
                    last_tail = duration = 0.0;
                    first_object = default(HitObject);
                }
            }
        }
    }
}