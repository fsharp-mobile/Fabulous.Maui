namespace Fabulous.Maui.Compatibility

open System.Runtime.CompilerServices
open Fabulous
open Fabulous.Maui
open Microsoft.Maui
open Microsoft.Maui.Controls

type IFabCompatIndicatorView =
    inherit IFabCompatTemplatedView
    inherit ITemplatedIndicatorView

module IndicatorView =
    let WidgetKey = CompatWidgets.register<IndicatorView>()

    let ItemsSource =
        Attributes.defineBindable<WidgetItems, System.Collections.Generic.IEnumerable<Widget>>
            IndicatorView.ItemsSourceProperty
            (fun modelValue ->
                seq {
                    for x in modelValue.OriginalItems do
                        modelValue.Template x
                })
            (fun a b -> ScalarAttributeComparers.equalityCompare a.OriginalItems b.OriginalItems)

    let HideSingle = Attributes.defineBindableBool IndicatorView.HideSingleProperty

    let IndicatorColor =
        Attributes.defineBindableAppThemeColor IndicatorView.IndicatorColorProperty

    let IndicatorSize =
        Attributes.defineBindableFloat IndicatorView.IndicatorSizeProperty

    let IndicatorsShape =
        Attributes.defineBindableEnum<IndicatorShape> IndicatorView.IndicatorsShapeProperty

    let MaximumVisible =
        Attributes.defineBindableInt IndicatorView.MaximumVisibleProperty

    let SelectedIndicatorColor =
        Attributes.defineBindableAppThemeColor IndicatorView.SelectedIndicatorColorProperty

[<AutoOpen>]
module IndicatorViewBuilders =
    type Fabulous.Maui.View with

        static member inline IndicatorView<'msg>(reference: ViewRef<IndicatorView>) =
            WidgetBuilder<'msg, IFabCompatIndicatorView>(IndicatorView.WidgetKey, ViewRefAttributes.ViewRef.WithValue(reference.Unbox))

[<Extension>]
type IndicatorViewModifiers =
    /// <summary>Sets the selected indicator color.</summary>
    /// <param name="light">The color of the indicator in the light theme.</param>
    /// <param name="dark">The color of the indicator in the dark theme.</param>
    [<Extension>]
    static member inline selectedIndicatorColor(this: WidgetBuilder<'msg, #IFabCompatIndicatorView>, light: FabColor, ?dark: FabColor) =
        this.AddScalar(IndicatorView.SelectedIndicatorColor.WithValue(AppTheme.create light dark))

    /// <summary>Sets the indicator size.</summary>
    /// <param name="size">The size of the indicator.</param>
    [<Extension>]
    static member inline indicatorSize(this: WidgetBuilder<'msg, #IFabCompatIndicatorView>, size: float) =
        this.AddScalar(IndicatorView.IndicatorSize.WithValue(size))

    /// <summary>Sets the indicator shape.</summary>
    /// <param name="shape">The shape of the indicator.</param>
    [<Extension>]
    static member inline indicatorsShape(this: WidgetBuilder<'msg, #IFabCompatIndicatorView>, shape: IndicatorShape) =
        this.AddScalar(IndicatorView.IndicatorsShape.WithValue(shape))

    [<Extension>]
    static member inline hideSingle(this: WidgetBuilder<'msg, #IFabCompatIndicatorView>, hide: bool) =
        this.AddScalar(IndicatorView.HideSingle.WithValue(hide))

    /// <summary>Sets the indicator color.</summary>
    /// <param name="light">The color of the indicator in the light theme.</param>
    /// <param name="dark">The color of the indicator in the dark theme.</param>
    [<Extension>]
    static member inline indicatorColor(this: WidgetBuilder<'msg, #IFabCompatIndicatorView>, light: FabColor, ?dark: FabColor) =
        this.AddScalar(IndicatorView.IndicatorColor.WithValue(AppTheme.create light dark))

    /// <summary>Sets the maximum number of visible indicators.</summary>
    /// <param name="maximum">The maximum number of visible indicators.</param>
    [<Extension>]
    static member inline maximumVisible(this: WidgetBuilder<'msg, IFabCompatIndicatorView>, count: int) =
        this.AddScalar(IndicatorView.MaximumVisible.WithValue(count))
