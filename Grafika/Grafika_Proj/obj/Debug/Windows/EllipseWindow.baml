   M S B A M L   `   `   `  ����U  QGrafika_Projekat_PR38_2019, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullW0clr-namespace:Grafika_Projekat_PR38_2019.Windows"Grafika_Projekat_PR38_2019.Windows  5   	   6    0Grafika_Projekat_PR38_2019.Windows.EllipseWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Blocal0clr-namespace:Grafika_Projekat_PR38_2019.Windows     5   	     � Title$  Draw Ellipse��5   	   $	��440��6   $	��400��6+    � WindowStartupLocation$ CenterScreen=�67   
�������.��5	      � .+�5
   
   �� $��Ellipse values��6   $��Center=�6)   $
��Left=�6M   $��	10,10,0,0q�6h   $	��Top=�6{   $	��440��6�   ���#0000006�   �� 5   
   $��X:��6   $
��Left=�6   $��	10,45,0,0q�68   $	��Top=�6K   $	��150��6c   ���#0000006o   �� 5   
   -   5         ��Name$
 x_tb��5      $
��Left=�6    $��Center=�6;   $��25��6]    �� TextWrapping$
 Wrap=�6i   $	��Top=�6}   $	��200��6�   $��
170,45,0,0q�6�   
����������#0000006�   �� 5   
   $��Y:��6   $
��Left=�6   $��	10,75,0,0q�68   $	��Top=�6K   $	��150��6c   ���#0000006o   �� 5   
   -   5        $
 y_tb��5      $
��Left=�6    $��Center=�6;   $��25��6]   $
 Wrap=�6i   $	��Top=�6}   $	��200��6�   $��
170,75,0,0q�6�   
����������#0000006�   �� 5   
   $��Stroke thickness:��6   $
��Left=�6,   $��
10,105,0,0q�6G   $	��Top=�6[   $	��150��6s   ���#0000006   �� 5   
   -   5        $ thickness_tb��5      $
��Left=�6(   $��Center=�6C   $��25��6e   $
 Wrap=�6q   $	��Top=�6�   $	��200��6�   $��170,105,0,0q�6�   
����������#0000006�   �� 5   
   $��Stroke color:��6   $
��Left=�6(   $��
10,135,0,0q�6C   $	��Top=�6W   $	��150��6o   ���#0000006{   �� 5   
   -   5        $��stroke_color_cb��5      $
��Left=�6*   $	��Top=�6E   $	��200��6]   $��170,136,0,0q�6i   �� 5   
   $��Fill color:��6   $
��Left=�6&   $��
10,165,0,0q�6A   $	��Top=�6U   $	��150��6m   ���#0000006y   �� 5   
   -   5         ��Name$ fill_color_cb��5      $
��Left=�6*   $	��Top=�6E   $	��200��6]   $��170,166,0,0q�6i   �� 5   
   $��Opacity:��6   $
��Left=�6#   $��
10,195,0,0q�6>   $	��Top=�6R   $	��150��6j   ���#0000006v   �� 5   
   -   5        $ 
opacity_tb��5      $
��Left=�6&   $��Center=�6A   $��25��6c   $
 Wrap=�6o   $	��Top=�6�   $	��200��6�   $��170,195,0,0q�6�   
����������#0000006�   �� 5   
   $��	Optional:��6   $
��Left=�6$   $��
10,265,0,0q�6?   $	��Top=�6S   $	��150��6k   ���#0000006w   �� 5   
   $��Text:��6   $
��Left=�6    $��
10,295,0,0q�6;   $	��Top=�6O   $	��150��6g   ���#0000006s   �� 5   
   -   5        $ text_tb��5      $
��Left=�6#   $��Center=�6>   $��25��6`   $
 Wrap=�6l   $	��Top=�6�   $	��200��6�   $��170,295,0,0q�6�   
����������#0000006�   �� 5   
   $��Text color:��6   $
��Left=�6&   $��
10,325,0,0q�6A   $	��Top=�6U   $	��150��6m   ���#0000006y   �� 5   
   -   5        $ text_color_cb��5      $
��Left=�6*   $	��Top=�6E   $	��200��6]   $��170,326,0,0q�6i   �� 5   
   -	   5         ��Name$ draw_btn��5      $
��Draw��6#   $
��Left=�62   $	��Top=�6M   $��75��6e   $��295,372,0,0q�6p   
����������#0000006�   �� 5    
   -
   5        $ quit_btn��5       $
��Quit��6#   $
��Left=�62   $	��Top=�6M   $��75��6e   $��198,372,0,0q�6p   
����������#0000006�   5!      5"      