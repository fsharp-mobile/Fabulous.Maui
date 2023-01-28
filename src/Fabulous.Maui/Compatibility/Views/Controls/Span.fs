namespace Fabulous.Maui.Compatibility

open System.Runtime.CompilerServices
open Fabulous
open Fabulous.Maui
open Microsoft.Maui
open Microsoft.Maui.Controls

type IFabCompatSpan =
    inherit IFabCompatGestureElement

module Span =
    let WidgetKey = CompatWidgets.register<Span>()

    let BackgroundColor =
        Attributes.defineBindableAppThemeColor Span.BackgroundColorProperty

    let CharacterSpacing = Attributes.defineBindableFloat Span.CharacterSpacingProperty

    let FontAttributes =
        Attributes.defineBindableEnum<FontAttributes> Span.FontAttributesProperty

    let FontFamily =
        Attributes.defineBindableWithEquality<string> Span.FontFamilyProperty

    let FontSize = Attributes.defineBindableFloat Span.FontSizeProperty

    let LineHeight = Attributes.defineBindableFloat Span.LineHeightProperty

    let Style = Attributes.defineBindableWithEquality<Style> Span.StyleProperty

    let TextColor = Attributes.defineBindableAppThemeColor Span.TextColorProperty

    let TextDecorations =
        Attributes.defineBindableEnum<TextDecorations> Span.TextDecorationsProperty

    let TextTransform =
        Attributes.defineBindableEnum<TextTransform> Span.TextTransformProperty

    let Text = Attributes.defineBindableWithEquality<string> Span.TextProperty

    let FontAutoScalingEnabled =
        Attributes.defineBindableBool Span.FontAutoScalingEnabledProperty

[<AutoOpen>]
module SpanBuilders =
    type Fabulous.Maui.View with

        static member inline Span<'msg>(text: string) =
            WidgetBuilder<'msg, IFabCompatSpan>(Span.WidgetKey, Span.Text.WithValue(text))

[<Extension>]
type SpanModifiers =

    [<Extension>]
    static member inline backgroundColor(this: WidgetBuilder<'msg, #IFabCompatSpan>, light: FabColor, ?dark: FabColor) =
        this.AddScalar(Span.BackgroundColor.WithValue(AppTheme.create light dark))

    [<Extension>]
    static member inline characterSpacing(this: WidgetBuilder<'msg, #IFabCompatSpan>, value: float) =
        this.AddScalar(Span.CharacterSpacing.WithValue(value))

    [<Extension>]
    static member inline font
        (
            this: WidgetBuilder<'msg, #IFabCompatSpan>,
            ?size: float,
            ?attributes: FontAttributes,
            ?fontFamily: string,
            ?autoScalingEnabled: bool
        ) =

        let mutable res = this

        match size with
        | None -> ()
        | Some v -> res <- res.AddScalar(Span.FontSize.WithValue(v))

        match attributes with
        | None -> ()
        | Some v -> res <- res.AddScalar(Span.FontAttributes.WithValue(v))

        match fontFamily with
        | None -> ()
        | Some v -> res <- res.AddScalar(Span.FontFamily.WithValue(v))

        match autoScalingEnabled with
        | None -> ()
        | Some v -> res <- res.AddScalar(Span.FontAutoScalingEnabled.WithValue(v))

        res

    [<Extension>]
    static member inline lineHeight(this: WidgetBuilder<'msg, #IFabCompatSpan>, value: float) =
        this.AddScalar(Span.LineHeight.WithValue(value))

    [<Extension>]
    static member inline style(this: WidgetBuilder<'msg, #IFabCompatSpan>, value: Style) =
        this.AddScalar(Span.Style.WithValue(value))

    [<Extension>]
    static member inline textColor(this: WidgetBuilder<'msg, #IFabCompatSpan>, light: FabColor, ?dark: FabColor) =
        this.AddScalar(Span.TextColor.WithValue(AppTheme.create light dark))

    [<Extension>]
    static member inline textDecorations(this: WidgetBuilder<'msg, #IFabCompatSpan>, value: TextDecorations) =
        this.AddScalar(Span.TextDecorations.WithValue(value))

    [<Extension>]
    static member inline textTransform(this: WidgetBuilder<'msg, #IFabCompatSpan>, value: TextTransform) =
        this.AddScalar(Span.TextTransform.WithValue(value))

    /// <summary>Link a ViewRef to access the direct Span control instance</summary>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFabCompatSpan>, value: ViewRef<Span>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))
