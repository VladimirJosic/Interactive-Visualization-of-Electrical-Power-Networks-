   M S B A M L   `   `   `  ����U  QGrafika_Projekat_PR38_2019, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:Grafika_Projekat_PR38_2019Grafika_Projekat_PR38_2019  5   	   +    %Grafika_Projekat_PR38_2019.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   :local(clr-namespace:Grafika_Projekat_PR38_2019     5   	     � Title$  Predmetni projekat��5   	   $
��1330��6$   $	��833��61    � WindowStartupLocation$ CenterScreen=�6>    � 
ResizeMode$ 	CanResize=�6c   
�������.��5	      ]� .+�5
   
   ��  �� Orientation$ Vertical=�6   .+�5      �� -   5         ��Name$ 
sliderZoom��5      $
��1325��6)   $��	0,10,0,10q�66    � Minimum$
 0.25Q�6I    � Value$ 1Q�6X    � Maximum$ 15Q�6b   
�������6p   �� 5      $ 
Horizontal=�6   .+�5      �� $ Vertical=�6   $	��125��64   .+�5      �� -   5         ��Name$ load_btn��5      $
��Load��6/   $��25��6>   $��0,5,0,0q�6J   
����������#0000006�   �� 5      $��Canvas size :��6   ���#000000$��Center=�6J   $��0, 45, 0 ,0q�6n   �� 5      -   5        	 ��Name$	 canvas_size_cb��5      67   �� 5      $��Matrix size :��6   ���#000000$��Center=�6I   $��
0, 5, 0 ,0q�6m   �� 5      -   5        
 ��Name$
 matrix_size��5      ���#000000$��Center=�6F   $��2q�6j   ���#000000$��30��6�   �� 5      $��Options��6   ���#000000$��Center=�6D   $��0, 50, 0 ,0q�6h   �� 5      -   5        $ draw_ellipse_btn��5      $��Draw elipse��67   $��
0, 5, 0 ,0q�6M   
����������#0000006�   �� 5      -   5        $ draw_polygon_btn��5      $��Draw polygon��67   $��
0, 5, 0 ,0q�6N   
����������#0000006�   �� 5      -   5        $ add_text_btn��5      $��Add text��63   $��
0, 5, 0 ,0q�6I   
����������#0000006�   �� 5      -   5        $ draw_grid_btn��5      $��Draw standard grid��64   $��0, 15, 0 ,0q�6Q   
����������#0000006�   �� 5      -	   5         ��Name$ nodes_checkbox��5      $��Without nodes��67   $��10(�6O   $��Center=�6]   
���   �$��
0, 5, 0, 0q�6�   �� 5      -
   5        $ draw_underground��5      $��Underground first��67   ���#000000
�������$��31��6{   6�   �� 5      $��Drawing time :��6   ���#000000$��Center=�6J   $��0, 10, 0 ,0q�6n   �� 5      -   5        $
 draw_lines_time��5      ���#000000$��Center=�6J   $��2q�6n   ���#000000$��30��6�   �� 5      $�� ��6   ���#000000$��Center=�6<   �� 5       -   5        $ undo_btn��5       $
��Undo��6/   $��
0, 5, 0 ,0q�6?   
����������#0000006{   �� 5!      -   5        $ redo_btn��5!      $
��Redo��6/   $��
0, 5, 0 ,0q�6?   
����������#0000006{   �� 5"      -   5        $ 	clear_btn��5"      $��Clear��60   $��
0, 5, 0 ,0q�6@   
����������#0000006|   5$      �� 5%      $
��Auto=�6   $
��Auto=�6D   $��10q�6g   $
��1125��6s   $	��750��6�   .��5&      �� -   5         ��Name$ 	customMap��5&      $
��1125��60   $	��750��6=   
������� � LayoutTransform 5'      �� 5(       �� ScaleX 6-   �� *Value+ �� ElementName$ 
sliderZoom�� �� ScaleY 6^   �� *Value+$ 
sliderZoom��5*      5+      5,      5-      5.      5/      