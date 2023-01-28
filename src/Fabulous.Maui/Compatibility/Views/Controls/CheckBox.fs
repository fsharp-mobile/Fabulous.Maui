namespace Fabulous.Maui.Compatibility

open System.Runtime.CompilerServices
open Fabulous
open Fabulous.Maui
open Microsoft.Maui
open Microsoft.Maui.Controls

type IFabCompatCheckBox =
    inherit IFabCompatView
    inherit ICheckBox

module CheckBox =

    let WidgetKey = CompatWidgets.register<CheckBox>()

    let Color = Attributes.defineBindableAppThemeColor CheckBox.ColorProperty

    let IsCheckedWithEvent =
        Attributes.defineBindableWithEvent "CheckBox_CheckedChanged" CheckBox.IsCheckedProperty (fun target -> (target :?> CheckBox).CheckedChanged)

[<AutoOpen>]
module CheckBoxBuilders =
    type Fabulous.Maui.View with

        static member inline CheckBox<'msg>(isChecked: bool, onCheckedChanged: bool -> 'msg) =
            WidgetBuilder<'msg, IFabCompatCheckBox>(
                CheckBox.WidgetKey,
                CheckBox.IsCheckedWithEvent.WithValue(ValueEventData.create isChecked (fun args -> onCheckedChanged args.Value |> box))
            )

[<Extension>]
type CheckBoxModifiers =
    /// <summary>Set the color of the checkBox.</summary>
    /// <param name="light">The color of the checkBox in the light theme.</param>
    /// <param name="dark">The color of the checkBox in the dark theme.</param>
    [<Extension>]
    static member inline color(this: WidgetBuilder<'msg, #IFabCompatCheckBox>, light: FabColor, ?dark: FabColor) =
        this.AddScalar(CheckBox.Color.WithValue(AppTheme.create light dark))

    /// <summary>Link a ViewRef to access the direct CheckBox control instance</summary>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFabCompatCheckBox>, value: ViewRef<CheckBox>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))
