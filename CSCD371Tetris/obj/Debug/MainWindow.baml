   M S B A M L   `   `   `  ����H  DCSCD371Tetris, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:CSCD371TetrisCSCD371Tetris  5   	       CSCD371Tetris.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:CSCD371Tetris     5   	   -   5          �Name$  window_tetris��5   	    � Title$ 
MainWindow��5   	   $��762.428��6   $��566.119��6-   
���   � � 
ResizeMode$ NoResize=�6P   .��5	      �  � ColumnDefinitions 5
   
   �� 5      $	��41* �6   �� 5      $	��15* �6   �� 5      $��0* �6   .+�5   
   �� -   5         ��Name$ rect_background_Copy��5      ���	#FF404144$��
-4,33,-5,0q�6C   ���	#FF404144$��Right=�6j   $	��569��6�   $��3��6�   �� 5   
   -   5        $ rect_background��5      ���	#FF404144$��
-4,33,-5,0q�6>   ���	#FF404144$��Right=�6e   $	��569��6�   $��3��6�   �� 5   
   -   5         ��Name$ canvas_playField��5      $
��Left=�6+   $	��693��6F   $��	26,33,0,0q�6S   $	��Top=�6f   $	��385��6~   ���	#FFC4CFA1$��2��6�   5      �� 5   
   -   5        $ canvas_upcoming_0��5      $��93��6,   $��	0,79,30,0q�68   $	��Top=�6K   ���	#FFC4CFA1$��Right=�6z   $��93��6�   $��1��6�   .+�5      �� -   5        $ rect_upcoming_1��5      $	��117��61   $	��116��6>   
 �� Top$	 -12��6J    �� Left$	 -12��6[   ��5      �� 5       �� ImageSource$ images/upcoming_frame.png��6!   5      5      �� 5   
   -   5        $ canvas_upcoming_1��5      $
��Left=�6,   $��54��6G   $��
27,210,0,0q�6S   $	��Top=�6g   $��54��6   ���	#FFC4CFA1$��1��6�   .+�5      �� -   5        $ rect_upcoming_2��5      $��70��61   $��70��6=   $ -8��6H   $ -8��6Y   ��5      �� 5      $ images/upcoming_frame.png��6!   5       5!      �� 5"   
   -	   5        $ canvas_upcoming_2��5"      $
��Left=�6,   $��54��6G   $��
27,285,0,0q�6S   $	��Top=�6g   $��54��6   ���	#FFC4CFA1$��1��6�   .+�5#      �� -
   5        $ rect_upcoming_3��5#      $��70��61   $ -8��6=   $ -5��6N   $��70��6^   ��5$      �� 5%      $ images/upcoming_frame.png��6!   5'      5(      �� 5)   
   -   5        $ canvas_upcoming_3��5)      $
��Left=�6,   $��54��6G   $��
27,365,0,0q�6S   $	��Top=�6g   $��54��6   ���	#FFC4CFA1	 J� RenderTransformOrigin$	 0.519,1.139&�6�   $��1��6�   .+�5*      �� -   5        $ rect_upcoming_4��5*      $��70��61   $ -8��6=   $ -8��6N   $��70��6^   ��5+      �� 5,      $ images/upcoming_frame.png��6!   5.      5/      �� 50   
   -   5        
 ��Name$
 txtblk_title��50      $
��Wrap=�6*   $��Definitely Not Tetris��6>   $��36(�6[    �� TextAlignment$ Center=�6i   $��26,169,0,435q�6�   ���	#FF404144$
��Bold"�6�   $)��#pack://appliction,,,/Font/#Emulogic)�6�   $
��Left=�6�   $	��385��6  $��2��6  �� 51   
   -   5        $
 txtblk_status_update��51      $��42��62   $
��Wrap=�6>   $��	Level Up!��6R   $ 99��6c   $ 10��6s   ���	#FF404144$��22(�6�   $)��#pack://appliction,,,/Font/#Emulogic)�6�   $ Center=�6�   $y�Hidden=�6�   $��26,303,0,388q�6  �� 52   
   $
��Left=�6   $��66��6/   $��	8,478,0,0q�6;   $	��Top=�6N   $	��142��6f   $��1��6r   ��53      �� 54      $! images/score_background.png��6   56      �� 57   
   -   5        $
 txtblk_highscore��57      $
��Wrap=�6.   $��High Scores��6B   $ Center=�6U   $��22(�6l   $��85,518,66,0q�6z   $��70��6�   $	��Top=�6�   ���	#FF404144$
��Bold"�6�   $)��#pack://appliction,,,/Font/#Emulogic)�6�   �� 58   
   -   5        $
 txtblk_pressESC��58      $
��Wrap=�6-   $��Press [ESC] to start��6A   $	 1.061,-0.312&�6]   $��20(�6�   $��85,365,0,276q�6�   $ Center=�6�   ���	#FF404144$
��Bold"�6�   $)��#pack://appliction,,,/Font/#Emulogic)�6�   $
��Left=�6  $	��266��62  �� 59   
   -   5        $ 
rect_score��59      $
��Left=�6(   $��44��6C   $��
21,442,0,0q�6O   $	��Top=�6c   $	��119��6{   $��1��6�   ��5:      �� 5;      $ images/score.png��6   5=      �� 5>   
   -   5        $
 txtblk_scoreInt��5>      $
��Left=�6-   $��
20,502,0,0q�6H   $
��Wrap=�6\   $��0��6p   $	��Top=�6y   ���	#FF414141$	��103��6�   $��34��6�   $��24(�6�   $)��#pack://appliction,,,/Font/#Emulogic)�6�   $��1��6�   o� 5?   
   $
��Left=�6   $��32��6*   $	��Top=�66   $	��560��6N   ���	#FF404144$��2��6q   .@�5@      m� -   5         m�Name$ mnuitm_play_pause��5@      $��_Play��62   $��32��6A   $	��100��6M   
�������6l   m� 5A      -   5        $ mnuitm_about��5A      $��About��6-   $��32��6<   $	��100��6H   
�������6g   5B      �� 5C   
   $
��Left=�6   $��58��6/   $��
12,582,0,0q�6;   $	��Top=�6O   $	��128��6g   $��1��6s   ��5D      �� 5E      $! images/lines_background.png��6   5G      �� 5H   
   -   5        $
 txtblk_linesInt��5H      $
��Left=�6-   $��
25,604,0,0q�6H   $
��Wrap=�6\   $��0��6p   $	��Top=�6y   ���	#FF434954$��98��6�   $��26��6�   $��24(�6�   $)��#pack://appliction,,,/Font/#Emulogic)�6�   $��1��6�   �� 5I   
   -   5        $
 txtblk_highscore01��5I      $
��Wrap=�60   $��High Scores��6D   $ Center=�6W   $��18(�6n   $��85,588,66,0q�6|   $��36��6�   $	��Top=�6�   ���	#FF404144$
��Bold"�6�   $)��#pack://appliction,,,/Font/#Emulogic)�6�   $y�Hidden=�6  �� 5J   
   -   5        $
 txtblk_highscore02��5J      $
��Wrap=�60   $��High Scores��6D   $ Center=�6W   $��18(�6n   $��85,623,66,0q�6|   $��36��6�   $	��Top=�6�   ���	#FF404144$
��Bold"�6�   $)��#pack://appliction,,,/Font/#Emulogic)�6�   $y�Hidden=�6  �� 5K   
   -   5        $
 txtblk_highscore03��5K      $
��Wrap=�60   $��High Scores��6D   $ Center=�6W   $��18(�6n   $��85,659,66,0q�6|   $��36��6�   $	��Top=�6�   ���	#FF404144$
��Bold"�6�   $)��#pack://appliction,,,/Font/#Emulogic)�6�   $y�Hidden=�6  �� 5L   
   $
��Left=�6   $��58��6/   $��
12,652,0,0q�6;   $	��Top=�6O   $	��128��6g   $��1��6s   ��5M      �� 5N      $! images/level_background.png��6   5P      �� 5Q   
   -   5        $
 txtblk_levelInt��5Q      $
��Left=�6-   $��
25,674,0,0q�6H   $
��Wrap=�6\   $��0��6p   $	��Top=�6y   ���	#FF434954$��98��6�   $��26��6�   $��24(�6�   $)��#pack://appliction,,,/Font/#Emulogic)�6�   $��1��6�   5R      5S      