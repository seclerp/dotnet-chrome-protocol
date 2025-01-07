// <auto-generated />
#nullable enable

namespace ChromeProtocol.Domains
{
  public static partial class Animation
  {
    /// <summary>Animation instance.</summary>
    /// <param name="Id">`Animation`&#39;s id.</param>
    /// <param name="Name">`Animation`&#39;s name.</param>
    /// <param name="PausedState">`Animation`&#39;s internal paused state.</param>
    /// <param name="PlayState">`Animation`&#39;s play state.</param>
    /// <param name="PlaybackRate">`Animation`&#39;s playback rate.</param>
    /// <param name="StartTime">
    /// `Animation`&#39;s start time.<br/>
    /// Milliseconds for time based animations and<br/>
    /// percentage [0 - 100] for scroll driven animations<br/>
    /// (i.e. when viewOrScrollTimeline exists).<br/>
    /// </param>
    /// <param name="CurrentTime">`Animation`&#39;s current time.</param>
    /// <param name="Type">Animation type of `Animation`.</param>
    /// <param name="Source">`Animation`&#39;s source animation node.</param>
    /// <param name="CssId">
    /// A unique ID for `Animation` representing the sources that triggered this CSS<br/>
    /// animation/transition.<br/>
    /// </param>
    /// <param name="ViewOrScrollTimeline">View or scroll timeline</param>
    public record AnimationType(
      [property: System.Text.Json.Serialization.JsonPropertyName("id")]
      string Id,
      [property: System.Text.Json.Serialization.JsonPropertyName("name")]
      string Name,
      [property: System.Text.Json.Serialization.JsonPropertyName("pausedState")]
      bool PausedState,
      [property: System.Text.Json.Serialization.JsonPropertyName("playState")]
      string PlayState,
      [property: System.Text.Json.Serialization.JsonPropertyName("playbackRate")]
      double PlaybackRate,
      [property: System.Text.Json.Serialization.JsonPropertyName("startTime")]
      double StartTime,
      [property: System.Text.Json.Serialization.JsonPropertyName("currentTime")]
      double CurrentTime,
      [property: System.Text.Json.Serialization.JsonPropertyName("type")]
      string Type,
      [property: System.Text.Json.Serialization.JsonPropertyName("source")]
      ChromeProtocol.Domains.Animation.AnimationEffectType? Source = default,
      [property: System.Text.Json.Serialization.JsonPropertyName("cssId")]
      string? CssId = default,
      [property: System.Text.Json.Serialization.JsonPropertyName("viewOrScrollTimeline")]
      ChromeProtocol.Domains.Animation.ViewOrScrollTimelineType? ViewOrScrollTimeline = default
    ) : ChromeProtocol.Core.IType
    {
    }
    /// <summary>Timeline instance</summary>
    /// <param name="Axis">Orientation of the scroll</param>
    /// <param name="SourceNodeId">Scroll container node</param>
    /// <param name="StartOffset">
    /// Represents the starting scroll position of the timeline<br/>
    /// as a length offset in pixels from scroll origin.<br/>
    /// </param>
    /// <param name="EndOffset">
    /// Represents the ending scroll position of the timeline<br/>
    /// as a length offset in pixels from scroll origin.<br/>
    /// </param>
    /// <param name="SubjectNodeId">
    /// The element whose principal box&#39;s visibility in the<br/>
    /// scrollport defined the progress of the timeline.<br/>
    /// Does not exist for animations with ScrollTimeline<br/>
    /// </param>
    public record ViewOrScrollTimelineType(
      [property: System.Text.Json.Serialization.JsonPropertyName("axis")]
      ChromeProtocol.Domains.DOM.ScrollOrientationType Axis,
      [property: System.Text.Json.Serialization.JsonPropertyName("sourceNodeId")]
      ChromeProtocol.Domains.DOM.BackendNodeIdType? SourceNodeId = default,
      [property: System.Text.Json.Serialization.JsonPropertyName("startOffset")]
      double? StartOffset = default,
      [property: System.Text.Json.Serialization.JsonPropertyName("endOffset")]
      double? EndOffset = default,
      [property: System.Text.Json.Serialization.JsonPropertyName("subjectNodeId")]
      ChromeProtocol.Domains.DOM.BackendNodeIdType? SubjectNodeId = default
    ) : ChromeProtocol.Core.IType
    {
    }
    /// <summary>AnimationEffect instance</summary>
    /// <param name="Delay">`AnimationEffect`&#39;s delay.</param>
    /// <param name="EndDelay">`AnimationEffect`&#39;s end delay.</param>
    /// <param name="IterationStart">`AnimationEffect`&#39;s iteration start.</param>
    /// <param name="Iterations">`AnimationEffect`&#39;s iterations.</param>
    /// <param name="Duration">
    /// `AnimationEffect`&#39;s iteration duration.<br/>
    /// Milliseconds for time based animations and<br/>
    /// percentage [0 - 100] for scroll driven animations<br/>
    /// (i.e. when viewOrScrollTimeline exists).<br/>
    /// </param>
    /// <param name="Direction">`AnimationEffect`&#39;s playback direction.</param>
    /// <param name="Fill">`AnimationEffect`&#39;s fill mode.</param>
    /// <param name="Easing">`AnimationEffect`&#39;s timing function.</param>
    /// <param name="BackendNodeId">`AnimationEffect`&#39;s target node.</param>
    /// <param name="KeyframesRule">`AnimationEffect`&#39;s keyframes.</param>
    public record AnimationEffectType(
      [property: System.Text.Json.Serialization.JsonPropertyName("delay")]
      double Delay,
      [property: System.Text.Json.Serialization.JsonPropertyName("endDelay")]
      double EndDelay,
      [property: System.Text.Json.Serialization.JsonPropertyName("iterationStart")]
      double IterationStart,
      [property: System.Text.Json.Serialization.JsonPropertyName("iterations")]
      double Iterations,
      [property: System.Text.Json.Serialization.JsonPropertyName("duration")]
      double Duration,
      [property: System.Text.Json.Serialization.JsonPropertyName("direction")]
      string Direction,
      [property: System.Text.Json.Serialization.JsonPropertyName("fill")]
      string Fill,
      [property: System.Text.Json.Serialization.JsonPropertyName("easing")]
      string Easing,
      [property: System.Text.Json.Serialization.JsonPropertyName("backendNodeId")]
      ChromeProtocol.Domains.DOM.BackendNodeIdType? BackendNodeId = default,
      [property: System.Text.Json.Serialization.JsonPropertyName("keyframesRule")]
      ChromeProtocol.Domains.Animation.KeyframesRuleType? KeyframesRule = default
    ) : ChromeProtocol.Core.IType
    {
    }
    /// <summary>Keyframes Rule</summary>
    /// <param name="Keyframes">List of animation keyframes.</param>
    /// <param name="Name">CSS keyframed animation&#39;s name.</param>
    public record KeyframesRuleType(
      [property: System.Text.Json.Serialization.JsonPropertyName("keyframes")]
      System.Collections.Generic.IReadOnlyList<ChromeProtocol.Domains.Animation.KeyframeStyleType> Keyframes,
      [property: System.Text.Json.Serialization.JsonPropertyName("name")]
      string? Name = default
    ) : ChromeProtocol.Core.IType
    {
    }
    /// <summary>Keyframe Style</summary>
    /// <param name="Offset">Keyframe&#39;s time offset.</param>
    /// <param name="Easing">`AnimationEffect`&#39;s timing function.</param>
    public record KeyframeStyleType(
      [property: System.Text.Json.Serialization.JsonPropertyName("offset")]
      string Offset,
      [property: System.Text.Json.Serialization.JsonPropertyName("easing")]
      string Easing
    ) : ChromeProtocol.Core.IType
    {
    }
    /// <summary>Event for when an animation has been cancelled.</summary>
    /// <param name="Id">Id of the animation that was cancelled.</param>
    [ChromeProtocol.Core.MethodName("Animation.animationCanceled")]
    public record AnimationCanceled(
      [property: System.Text.Json.Serialization.JsonPropertyName("id")]
      string Id
    ) : ChromeProtocol.Core.IEvent
    {
    }
    /// <summary>Event for each animation that has been created.</summary>
    /// <param name="Id">Id of the animation that was created.</param>
    [ChromeProtocol.Core.MethodName("Animation.animationCreated")]
    public record AnimationCreated(
      [property: System.Text.Json.Serialization.JsonPropertyName("id")]
      string Id
    ) : ChromeProtocol.Core.IEvent
    {
    }
    /// <summary>Event for animation that has been started.</summary>
    /// <param name="Animation">Animation that was started.</param>
    [ChromeProtocol.Core.MethodName("Animation.animationStarted")]
    public record AnimationStarted(
      [property: System.Text.Json.Serialization.JsonPropertyName("animation")]
      ChromeProtocol.Domains.Animation.AnimationType Animation
    ) : ChromeProtocol.Core.IEvent
    {
    }
    /// <summary>Event for animation that has been updated.</summary>
    /// <param name="Animation">Animation that was updated.</param>
    [ChromeProtocol.Core.MethodName("Animation.animationUpdated")]
    public record AnimationUpdated(
      [property: System.Text.Json.Serialization.JsonPropertyName("animation")]
      ChromeProtocol.Domains.Animation.AnimationType Animation
    ) : ChromeProtocol.Core.IEvent
    {
    }
    /// <summary>Disables animation domain notifications.</summary>
    public static ChromeProtocol.Domains.Animation.DisableRequest Disable()    
    {
      return new ChromeProtocol.Domains.Animation.DisableRequest();
    }
    /// <summary>Disables animation domain notifications.</summary>
    [ChromeProtocol.Core.MethodName("Animation.disable")]
    public record DisableRequest() : ChromeProtocol.Core.ICommand<DisableRequestResult>
    {
    }
    public record DisableRequestResult() : ChromeProtocol.Core.IType
    {
    }
    /// <summary>Enables animation domain notifications.</summary>
    public static ChromeProtocol.Domains.Animation.EnableRequest Enable()    
    {
      return new ChromeProtocol.Domains.Animation.EnableRequest();
    }
    /// <summary>Enables animation domain notifications.</summary>
    [ChromeProtocol.Core.MethodName("Animation.enable")]
    public record EnableRequest() : ChromeProtocol.Core.ICommand<EnableRequestResult>
    {
    }
    public record EnableRequestResult() : ChromeProtocol.Core.IType
    {
    }
    /// <summary>Returns the current time of the an animation.</summary>
    /// <param name="Id">Id of animation.</param>
    public static ChromeProtocol.Domains.Animation.GetCurrentTimeRequest GetCurrentTime(string Id)    
    {
      return new ChromeProtocol.Domains.Animation.GetCurrentTimeRequest(Id);
    }
    /// <summary>Returns the current time of the an animation.</summary>
    /// <param name="Id">Id of animation.</param>
    [ChromeProtocol.Core.MethodName("Animation.getCurrentTime")]
    public record GetCurrentTimeRequest(
      [property: System.Text.Json.Serialization.JsonPropertyName("id")]
      string Id
    ) : ChromeProtocol.Core.ICommand<GetCurrentTimeRequestResult>
    {
    }
    /// <param name="CurrentTime">Current time of the page.</param>
    public record GetCurrentTimeRequestResult(
      [property: System.Text.Json.Serialization.JsonPropertyName("currentTime")]
      double CurrentTime
    ) : ChromeProtocol.Core.IType
    {
    }
    /// <summary>Gets the playback rate of the document timeline.</summary>
    public static ChromeProtocol.Domains.Animation.GetPlaybackRateRequest GetPlaybackRate()    
    {
      return new ChromeProtocol.Domains.Animation.GetPlaybackRateRequest();
    }
    /// <summary>Gets the playback rate of the document timeline.</summary>
    [ChromeProtocol.Core.MethodName("Animation.getPlaybackRate")]
    public record GetPlaybackRateRequest() : ChromeProtocol.Core.ICommand<GetPlaybackRateRequestResult>
    {
    }
    /// <param name="PlaybackRate">Playback rate for animations on page.</param>
    public record GetPlaybackRateRequestResult(
      [property: System.Text.Json.Serialization.JsonPropertyName("playbackRate")]
      double PlaybackRate
    ) : ChromeProtocol.Core.IType
    {
    }
    /// <summary>Releases a set of animations to no longer be manipulated.</summary>
    /// <param name="Animations">List of animation ids to seek.</param>
    public static ChromeProtocol.Domains.Animation.ReleaseAnimationsRequest ReleaseAnimations(System.Collections.Generic.IReadOnlyList<string> Animations)    
    {
      return new ChromeProtocol.Domains.Animation.ReleaseAnimationsRequest(Animations);
    }
    /// <summary>Releases a set of animations to no longer be manipulated.</summary>
    /// <param name="Animations">List of animation ids to seek.</param>
    [ChromeProtocol.Core.MethodName("Animation.releaseAnimations")]
    public record ReleaseAnimationsRequest(
      [property: System.Text.Json.Serialization.JsonPropertyName("animations")]
      System.Collections.Generic.IReadOnlyList<string> Animations
    ) : ChromeProtocol.Core.ICommand<ReleaseAnimationsRequestResult>
    {
    }
    public record ReleaseAnimationsRequestResult() : ChromeProtocol.Core.IType
    {
    }
    /// <summary>Gets the remote object of the Animation.</summary>
    /// <param name="AnimationId">Animation id.</param>
    public static ChromeProtocol.Domains.Animation.ResolveAnimationRequest ResolveAnimation(string AnimationId)    
    {
      return new ChromeProtocol.Domains.Animation.ResolveAnimationRequest(AnimationId);
    }
    /// <summary>Gets the remote object of the Animation.</summary>
    /// <param name="AnimationId">Animation id.</param>
    [ChromeProtocol.Core.MethodName("Animation.resolveAnimation")]
    public record ResolveAnimationRequest(
      [property: System.Text.Json.Serialization.JsonPropertyName("animationId")]
      string AnimationId
    ) : ChromeProtocol.Core.ICommand<ResolveAnimationRequestResult>
    {
    }
    /// <param name="RemoteObject">Corresponding remote object.</param>
    public record ResolveAnimationRequestResult(
      [property: System.Text.Json.Serialization.JsonPropertyName("remoteObject")]
      ChromeProtocol.Domains.Runtime.RemoteObjectType RemoteObject
    ) : ChromeProtocol.Core.IType
    {
    }
    /// <summary>Seek a set of animations to a particular time within each animation.</summary>
    /// <param name="Animations">List of animation ids to seek.</param>
    /// <param name="CurrentTime">Set the current time of each animation.</param>
    public static ChromeProtocol.Domains.Animation.SeekAnimationsRequest SeekAnimations(System.Collections.Generic.IReadOnlyList<string> Animations, double CurrentTime)    
    {
      return new ChromeProtocol.Domains.Animation.SeekAnimationsRequest(Animations, CurrentTime);
    }
    /// <summary>Seek a set of animations to a particular time within each animation.</summary>
    /// <param name="Animations">List of animation ids to seek.</param>
    /// <param name="CurrentTime">Set the current time of each animation.</param>
    [ChromeProtocol.Core.MethodName("Animation.seekAnimations")]
    public record SeekAnimationsRequest(
      [property: System.Text.Json.Serialization.JsonPropertyName("animations")]
      System.Collections.Generic.IReadOnlyList<string> Animations,
      [property: System.Text.Json.Serialization.JsonPropertyName("currentTime")]
      double CurrentTime
    ) : ChromeProtocol.Core.ICommand<SeekAnimationsRequestResult>
    {
    }
    public record SeekAnimationsRequestResult() : ChromeProtocol.Core.IType
    {
    }
    /// <summary>Sets the paused state of a set of animations.</summary>
    /// <param name="Animations">Animations to set the pause state of.</param>
    /// <param name="Paused">Paused state to set to.</param>
    public static ChromeProtocol.Domains.Animation.SetPausedRequest SetPaused(System.Collections.Generic.IReadOnlyList<string> Animations, bool Paused)    
    {
      return new ChromeProtocol.Domains.Animation.SetPausedRequest(Animations, Paused);
    }
    /// <summary>Sets the paused state of a set of animations.</summary>
    /// <param name="Animations">Animations to set the pause state of.</param>
    /// <param name="Paused">Paused state to set to.</param>
    [ChromeProtocol.Core.MethodName("Animation.setPaused")]
    public record SetPausedRequest(
      [property: System.Text.Json.Serialization.JsonPropertyName("animations")]
      System.Collections.Generic.IReadOnlyList<string> Animations,
      [property: System.Text.Json.Serialization.JsonPropertyName("paused")]
      bool Paused
    ) : ChromeProtocol.Core.ICommand<SetPausedRequestResult>
    {
    }
    public record SetPausedRequestResult() : ChromeProtocol.Core.IType
    {
    }
    /// <summary>Sets the playback rate of the document timeline.</summary>
    /// <param name="PlaybackRate">Playback rate for animations on page</param>
    public static ChromeProtocol.Domains.Animation.SetPlaybackRateRequest SetPlaybackRate(double PlaybackRate)    
    {
      return new ChromeProtocol.Domains.Animation.SetPlaybackRateRequest(PlaybackRate);
    }
    /// <summary>Sets the playback rate of the document timeline.</summary>
    /// <param name="PlaybackRate">Playback rate for animations on page</param>
    [ChromeProtocol.Core.MethodName("Animation.setPlaybackRate")]
    public record SetPlaybackRateRequest(
      [property: System.Text.Json.Serialization.JsonPropertyName("playbackRate")]
      double PlaybackRate
    ) : ChromeProtocol.Core.ICommand<SetPlaybackRateRequestResult>
    {
    }
    public record SetPlaybackRateRequestResult() : ChromeProtocol.Core.IType
    {
    }
    /// <summary>Sets the timing of an animation node.</summary>
    /// <param name="AnimationId">Animation id.</param>
    /// <param name="Duration">Duration of the animation.</param>
    /// <param name="Delay">Delay of the animation.</param>
    public static ChromeProtocol.Domains.Animation.SetTimingRequest SetTiming(string AnimationId, double Duration, double Delay)    
    {
      return new ChromeProtocol.Domains.Animation.SetTimingRequest(AnimationId, Duration, Delay);
    }
    /// <summary>Sets the timing of an animation node.</summary>
    /// <param name="AnimationId">Animation id.</param>
    /// <param name="Duration">Duration of the animation.</param>
    /// <param name="Delay">Delay of the animation.</param>
    [ChromeProtocol.Core.MethodName("Animation.setTiming")]
    public record SetTimingRequest(
      [property: System.Text.Json.Serialization.JsonPropertyName("animationId")]
      string AnimationId,
      [property: System.Text.Json.Serialization.JsonPropertyName("duration")]
      double Duration,
      [property: System.Text.Json.Serialization.JsonPropertyName("delay")]
      double Delay
    ) : ChromeProtocol.Core.ICommand<SetTimingRequestResult>
    {
    }
    public record SetTimingRequestResult() : ChromeProtocol.Core.IType
    {
    }
  }
}
