   M S B A M L   `   `   `  ???? B  >ToolBox, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:ToolBoxToolBox  5clr-namespace:ToolBox.ResourcesToolBox.Resources  +clr-namespace:ToolBox.ViewToolBox.View      ToolBox.View.Timer   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     'localclr-namespace:ToolBox     .rsclr-namespace:ToolBox.Resources     +viewclr-namespace:ToolBox.View     -     >?Name$  userControl??z?T? -   	trans?? ? Triggers :?  :? RoutedEvent$ Loaded?? :? 
SourceName$ userControl??.[??? .???? .??U?  ?? TargetProperty$ Y? ?? 
TargetName$ trans??.\???  O? KeyTime$ 0:0:0?? O? Value$ 50Q??? $ 0:0:0.5??$ 0Q?W? $ userControl??$ Opacity? W? From 0		 W? To	 1
 g? Duration$
 0:0:0.4D? ? 	Resources    ToolBox.FontSizeConverter   FontSizeConverter'   !ToolBox.View.TimeBarSizeConverter  TimeBarSizeConverter&    ToolBox.View.StartPointConverter  StartPointConverter??%-   &	        &	      &	 
           .??? -    ?Name$
 grid??
??????????? *ActualWidth+ ?? ElementName$ userControl?? ?? Mode$ OneWay=????? *ActualHeight+$ userControl??$ OneWay=?.+??? ???? *size+?? -    ??Name$ TimeBar??$??Center=?$??Center=? K? ZIndex$ 1?? ? ToolTip ?? *time+???? *ActualWidth+$ TimeBar?? ?? 	Converter# [  $ OneWay=????? *ActualHeight+# [ $
 grid??$ OneWay=????? *ActualHeight+$ TimeBar??$ OneWay=????? .R?? $ grid1?????? *ActualWidth+$ grid1??$ OneWay=?.+?? $
 grid??
???????$	??280??$	??280??$??Center=?$??Center=????? *ActualWidth+$ grid1??$ OneWay=????? $ grid1??$ OneWay=? '   !ToolBox.View.EllipseSizeConverter  EllipseSizeConverter??%   &	          J? Clip @?  @? RadiusX ?? *ActualWidth+# [ $
 grid??$ OneWay=? @? RadiusY ?? *ActualWidth+# [ $
 grid??$ OneWay=? ? 	Transform T?  T? Y ?? *X+ ?? RelativeSource ?? *Self+ T? X ?? *ActualWidth+# [ $
 grid??$ OneWay=?.+?A?  A?Name$ bg??
???????#??z??$??10??$??Center=?$??Center=????? *ActualWidth+$ OneWay=? ?? *Self+F? ???	#FF288EFF$??10?? ?? StrokeStartLineCap$ Round=? ?? StrokeEndLineCap$ Round=?#??z??#??z????B? .??E?  E? 
StartPoint ?? *ActualWidth+$
 grid??# [ .???? ro?? ?? SweepDirection$ 	Clockwise=? ?? 
IsLargeArc ?? *	IsLarge+  ?? Size  ?? *size+! ?? Point! ?? *
EndPoint+?? $??Center=?$??Center=?$ 1??#??z??#??z 