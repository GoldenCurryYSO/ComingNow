   M S B A M L   `   `   `  ����D  @ComingNow, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:ComingNow	ComingNow  5   	   9!clr-namespace:ComingNow.ViewModelComingNow.ViewModel  5   	   O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5   	       ComingNow.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:ComingNow     5   	   0vm!clr-namespace:ComingNow.ViewModel     5   	   6sys&clr-namespace:System;assembly=mscorlib     5   	     � Title$  	ComingNow��5
   	   $	��550��6   $	��525��6(    � WindowStyle$ SingleBorderWindow=�5   	    � AllowsTransparency .  
������  � 
ResizeMode$ 	CanResize=�6^    � DataContext 5      -  'ComingNow.ViewModel.MainWindowViewModel  5   
    � 	Resources 5         ScrollBarSize  ScrollBarRepeatButtonSize  ScrollBarMarginSize  ScrollBarColorBrush �� Color  ScrollBarBorderBrush  ScrollBarTrackBrush  ScrollBarHilightBrush  ScrollBarPressedBrush  ScrollBarDisabledBrush 	 ScrollBarRepeatButtonStyle �� 
TargetType �� Property	 �� Value
 �� 
TargetType ��Name �� CornerRadius �� RelativeSource �� Triggers P� Property P� Value �� 
TargetName 
 ScrollBarTrackStyle  ScrollBarThumbStyle  CustomScrollBarStyle �� Triggers u� Left
 u� Top u� Right u� Bottom � RowDefinitions ]�Name ]� DecreaseRepeatButton ]� Thumb ]� IncreaseRepeatButton � ColumnDefinitions  CustomScrollViewerStyle ��Name �� Orientation �� VerticalOffset  � Value! �� ScrollableHeight" � Maximum# �� ViewportHeight$ �� ViewportSize*% �� #ComputedVerticalScrollBarVisibility& �� HorizontalOffset' �� ScrollableWidth( �� ViewportWidth,) �� %ComputedHorizontalScrollBarVisibility��%  &	        &	      &	 2     &	 K     &	 n     &	 �     &	 �     &	 �     &	 �     &		     7  7  7  7  7  &	
     &	 �    7  7  &	 E    7   7  7  7  7  7  7  7 	 7  7 
 7  7 
 7 	 7  7   7  7  7  7  7  7  7 	 7  7 
 7  7 
 7 	 7  &	 �    7  7  7  7  7  7  '�� �    X�5   
   8Q�6.   X�5   
   8Q�6:   X�5   
   1Q�64   �� 5   
   $ 	#66ffffff��66   �� 5   
   $ 	#66ffffff��67   �� 5   
   $ 	#33ffffff��66   �� 5   
   $ 	#ccffffff��68   �� 5!   
   $ 	#99ffffff��68   �� 5#   
   $ 	#44ffffff��69   �� 5&   
   " ��63   .�5'      ��  � ��	 5(      �� 5)      "
 ��6&   .R�5*      �� $ Border��6!   $��0q�61   $ 0��6<   8��  8�� $��1q�6�   ._�5+      F� $��Center=�6#   $��Center=�6@   8�� ��6�   �� *	Content+ �� *TemplatedParent+5,       5-      P� 5.       � �� . . �5/   "   �� $ Border��6)   8	   � ��6S   50      P� 51       � {� .  . �52   "   �� 8	   � ��6?   53      55      57      58      �� 5;   
   " ��6,   .�5<      ��  � ��	 5=      �� 5>      "
 ��6&   .R�5?      �� 
������ 6!   5@      5B      5C      �� 5F   
   " n�6,   .�5G      ��  � ��	 5H      �� 5I      "
 n�6&   .R�5J      �� $ 0��6!   8��  8�� $��1q�6�   5K      5M      5N      �� 5Q   
   " ��6-   .�5R      ��  �@�OverridesDefaultStyle	 . 66    5S      P� 5U       �@��Orientation$ Vertical=�61   . �5V      ��  � ��8	   6.   �� 5W       � ��$
	 Auto��6/   �� 5X       � ��	 5Y      u� 5Z      $ 0Q�6(   8  8  8  6�   5\      �� 5]       � ��	 5^      �� 5_      .R�5`   "   �  5a   &   �� 5b   *   8�� 68   �� 5c   *   �� 5d   *   8�� 68   .+�5f   &   �� $��3��6-   $ 0��6>   8�� 6O   �� 5g   &   $��0��63   8�� 8�� $��ScrollBar.LineUpCommand��6�   $��M 0 4 L 8 4 L 4 0 Z��6�   ]� 5h   &   $ 
PART_Track��6,   $��1��6@   ��.  5i   *   �� 5j   .   8��	 $��ScrollBar.PageUpCommand��6i    5l   *   n� 5m   .   8��
 $��0,1,0,1q�6b    5o   *   �� 5p   .   8�� $��ScrollBar.PageDownCommand��6h   5r   '   �� 5s   &   $��2��63   8�� 8�� $��ScrollBar.LineDownCommand��6�   $��M 0 0 L 4 4 L 8 0 Z��6�   5t   #   5u      5w      5x      P� 5z       �@��Orientation$ 
Horizontal=�61   . �5{      ��  � ��$
	 Auto��6.   �� 5|       � ��8	  6/   �� 5}       � ��	 5~      u� 5      8  $ 0Q�6T   8  8  6�   5�      �� 5�       � ��	 5�      �� 5�      .R�5�   "   �  5�   &   �� 5�   *   8�� 6;   �� 5�   *   �� 5�   *   8�� 6;   .+�5�   &   �� $��3��6-   $ 0��6A   8�� 6R   �� 5�   &   $��0��63   8�� 8�� $��ScrollBar.LineLeftCommand��6�   $��M 4 0 L 4 8 L 0 4 Z��6�   ]� 5�   &   $ 
PART_Track��6,   $��1��6@   ��.   5�   *   �� 5�   .   8�� $��ScrollBar.PageLeftCommand��6i    5�   *   n� 5�   .   8�� $��1,0,1,0q�6b    5�   *   �� 5�   .   8�� $ ��ScrollBar.PageRightCommand��6h   5�   '   �� 5�   &   $��2��63   8�� 8�� $ ��ScrollBar.LineRightCommand��6�   $��M 0 0 L 4 4 L 0 8 Z��6�   5�   #   5�      5�      5�      5�      �� 5�   
   " ��60   .�5�      ��  � ��	 5�      �� 5�      "
 ��6&   .R�5�      �  5�      �� 5�   "   �� 5�   "   $
��Auto �63    5�      �� 5�   "   �� 5�   "   $
��Auto �60   .+�5�      �� $��0��65   $��0��6E   ��5�   "   u� 5�   &   8   8  8  8  6�   5�      �� 5�      $ PART_VerticalScrollBar��6(   $��1��6H   $��0��6X   $ Vertical=�6e   #  z 6|   #" z! 6�   #$ z# 6�   #y�z% 6  8�� 6E  �� 5�      $ PART_HorizontalScrollBar��6(   $��0��6J   $��1��6Z   $ 
Horizontal=�6g   #  z& 6�   #" z' 6�   #$ z( 6�   #y�z) 6  8�� 6K  �� 5�      $��1��6%   $��1��65   5�      5�      5�      5�      �� 5�   
   " ��6   .�5�      ��  � ��
	 �����6;   5�      5�      .��5�      �� -   5        $��Columns��5�      * �� Orientation$* 
Horizontal=�6    .+�5�   
   �� $* Vertical=�6   $��30��6,   .+�5�      �� -   5        $��30��5�   0   6>   5�      5�      5�      