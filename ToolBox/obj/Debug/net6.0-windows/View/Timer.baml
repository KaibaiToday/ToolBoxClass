   M S B A M L   `   `   `  ????B  >ToolBox, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:ToolBoxToolBox  5      5clr-namespace:ToolBox.ResourcesToolBox.Resources  5      +clr-namespace:ToolBox.ViewToolBox.View  5          ToolBox.View.Timer   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      'localclr-namespace:ToolBox     5      .rsclr-namespace:ToolBox.Resources     5      +viewclr-namespace:ToolBox.View     5      -   5          >?Name$  userControl??5      z?5      T? 5   
   -   5        	trans??5       ? Triggers 5      :? 5   
    :? RoutedEvent$ Loaded??6    :? 
SourceName$ userControl??6,   .[?5      ?? .??5      ?? .??5      U?  ?? TargetProperty$ Y?64    ?? 
TargetName$ trans??6R   .\?5      ??  O? KeyTime$ 0:0:0??6/    O? Value$ 50Q?6?   ?? 5      $ 0:0:0.5??6/   $ 0Q?6A   5      W? 5      $ userControl??6&   $ Opacity?6J    W? From 06n   		 W? To	 16w   
 g? Duration$
 0:0:0.4D?6~   5      5      5       ? 	Resources 5         ToolBox.FontSizeConverter   FontSizeConverter'   !ToolBox.View.TimeBarSizeConverter  TimeBarSizeConverter&    ToolBox.View.StartPointConverter  StartPointConverter??%W   &	        &	      &	 &       5   
   6"     5   
   6$     5   
   6#   5      .??5       ? -   5         ?Name$
 grid??5       
?????????6,   ?? *ActualWidth+ ?? ElementName$ userControl?? ?? Mode$ OneWay=???6p   ?? *ActualHeight+$ userControl??$ OneWay=?.+?5!   
   ?? ??6   ?? *size+?? 5"   
   -   5         ??Name$ TimeBar??5"      $??Center=?6:   $??Center=?6W    K? ZIndex$ 1??6?    ? ToolTip 6!   ?? *time+??6r   ?? *ActualWidth+$ TimeBar?? ?? 	Converter# [  $ OneWay=???6?   ?? *ActualHeight+# [ $
 grid??$ OneWay=???6a  ?? *ActualHeight+$ TimeBar??$ OneWay=???5#      ?? 5$      .R?5%      ? $ grid1??6   ??6*   ?? *ActualWidth+$ grid1??$ OneWay=?.+?5&      ? $
 grid??6Q   
???????$	??280??62   $	??280??6A   $??Center=?6_   $??Center=?6|   ??6?   ?? *ActualWidth+$ grid1??$ OneWay=???6?   ?? $ grid1??$ OneWay=? 5'      '   !ToolBox.View.EllipseSizeConverter  EllipseSizeConverter??%   &	         5(   "   6<   5)       J? Clip 5*      @? 5+   "    @? RadiusX 62   ?? *ActualWidth+# [ $
 grid??$ OneWay=? @? RadiusY 6?   ?? *ActualWidth+# [ $
 grid??$ OneWay=? ? 	Transform 5,   &   T? 5-   *    T? Y 6=   ?? *X+ ?? RelativeSource ?? *Self+ T? X 6s   ?? *ActualWidth+# [ $
 grid??$ OneWay=?5/   #   .+?51      A?  A?Name$ bg??6&   
???????#??z??6A   $??10??6?   $??Center=?6?   $??Center=?6?   ??6a   ?? *ActualWidth+$ OneWay=? ?? *Self+6?   F? 52      ???	#FF288EFF$??10??66    ?? StrokeStartLineCap$ Round=?6K    ?? StrokeEndLineCap$ Round=?6f   #??z??6   #??z??6?   ??53   "   B? 54   &   .??55   *   E?  E? 
StartPoint 65   ?? *ActualWidth+$
 grid??# [ .??56   .   ?? ro?? ?? SweepDirection$ 	Clockwise=?6E    ?? 
IsLargeArc 6`   ?? *	IsLarge+  ?? Size  6   ?? *size+! ?? Point! 6?   ?? *
EndPoint+57   +   58   '   5:      ?? 5;      $??Center=?6$   $??Center=?6A   $ 1??6\   #??z??6m   #??z 6?   5<      5=      5>      5@      5A      5B      