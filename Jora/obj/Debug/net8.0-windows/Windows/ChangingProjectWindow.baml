   M S B A M L   `   `   `  ����?  ;Jora, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:JoraJora  5   	        Jora.ChangingProjectWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   $localclr-namespace:Jora     5   	     � Title$  ChangingProjectWindow��5   	   $	��800��6'   $	��600��64   .��5	      � $	��600��6   $	��800��6   .+�5
   
   �� $��Project name:��6   $��Center=�6(   $
��Left=�6J   $��40��6e   $��	10,10,0,0q�6q   $	��Top=�6�   $	��388��6�   $��Arial)�6�   $��24(�6�   �� 5   
   -   5         ��Name$ 
txtbx_Name��5      $��Center=�6&   $
��Left=�6H   $��40��6c   $��	10,50,0,0q�6o    �� TextWrapping$
 Wrap=�6�   $	��Top=�6�   $	��560��6�   $��Arial)�6�   $��24(�6�   �� 5   
   $��Project description:��6   $��Center=�6/   $
��Left=�6Q   $��40��6l   $��
10,122,0,0q�6x   $	��Top=�6�   $	��217��6�   $��Arial)�6�   $��24(�6�   �� 5   
   -   5        $ txtbx_Description��5      $
 Wrap=�6-   $ �� HorizontalScrollBarVisibility$
 Auto=�6A   $
��Left=�6f   $	��160��6�   $��
10,162,0,0q�6�   $	��Top=�6�   $	��560��6�   $��Arial)�6�   $��24(�6�   �� 5   
   $��Project deadline:��6   $��Center=�6,   $
��Left=�6N   $��40��6i   $��
10,360,0,0q�6u   $	��Top=�6�   $	��189��6�   $��Arial)�6�   $��24(�6�   �� 5   
   -   5        $ txtbx_Deadline��5      $��Center=�6*   $
��Left=�6L   $��40��6g   $��
10,400,0,0q�6s   $
 Wrap=�6�   $	��Top=�6�   $	��560��6�   $��Arial)�6�   $��24(�6�   �� 5   
   -   5         ��Name$ btn_DeleteProject��5      $��Delete project��6,   $��Center=�6E   $��70��6b   $��	0,690,0,0q�6n   $	��Top=�6�   $	��560��6�   ���	#FFCBF1FF$��24(�6�   $��Arial)�6�   
���  ��6�   �� 5   
   -   5        $ 
btn_Cancel��5      $��Cancel��6%   $
��Left=�66   $��70��6Q   $��318,602,0,0q�6]   $	��Top=�6r   $	��262��6�   ���	#FFCBF1FF$��24(�6�   $��Arial)�6�   
���   �6�   �� 5   
   -   5        $ btn_SaveChanges��5      $��Save Changes��6*   $
��Left=�6A   $��70��6\   $��
20,602,0,0q�6h   $	��Top=�6|   $	��262��6�   ���	#FFCBF1FF$��24(�6�   $��Arial)�6�   
���   �6�   �� 5   
   $0��*Project creation date (cannot be changed):��6   $��Center=�6E   $
��Left=�6g   $��40��6�   $��
10,474,0,0q�6�   $	��Top=�6�   $	��464��6�   $��Arial)�6�   $��24(�6�   �� 5   
   -   5        $ txtbx_CreationDate��5      $��Center=�6.   $
��Left=�6P   $��40��6k   $��
10,514,0,0q�6w   $
 Wrap=�6�   $	��Top=�6�   $	��560��6�   $��Arial)�6�   $��24(�6�    �� 
IsReadOnly$
 True��6�   5      5      