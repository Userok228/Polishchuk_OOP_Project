   M S B A M L   `   `   `  ����?  ;Jora, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:JoraJora  5   	       Jora.TaskInfoWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   $localclr-namespace:Jora     5   	     � Title$  TaskInfoWindow��5   	   $	��800��6    $	��600��6-   .��5	      � $	��600��6   $	��800��6   .+�5
   
   �� $��
Task name:��6   $��Center=�6%   $
��Left=�6G   $��40��6b   $��	20,20,0,0q�6n   $	��Top=�6�   $	��136��6�   $��Arial)�6�   $��24(�6�   �� 5   
   -   5         ��Name$ 
txtbx_Name��5      $��Center=�6&   $
��Left=�6H   $��40��6c   $��	20,60,0,0q�6o    �� TextWrapping$
 Wrap=�6�   $	��Top=�6�   $	��560��6�   $��Arial)�6�   $��24(�6�    �� 
IsReadOnly$
 True��6�   �� 5   
   $��Task description:��6   $��Center=�6,   $
��Left=�6N   $��40��6i   $��
20,120,0,0q�6u   $	��Top=�6�   $	��210��6�   $��Arial)�6�   $��24(�6�   �� 5   
   -   5        $ txtbx_Description��5      $
 Wrap=�6-   $ �� HorizontalScrollBarVisibility$
 Auto=�6A   $��Center=�6f   $	��160��6�   $��	0,160,0,0q�6�   $	��Top=�6�   $	��560��6�   $��Arial)�6�   $��24(�6�   $
 True��6�   �� 5   
   $��Creation Date:��6   $��Center=�6)   $
��Left=�6K   $��40��6f   $��
20,340,0,0q�6r   $	��Top=�6�   $	��270��6�   $��Arial)�6�   $��24(�6�   �� 5   
   -   5        $ txtbx_CreationDate��5      $��Center=�6.   $
��Left=�6P   $��40��6k   $
 Wrap=�6w   $��Center=�6�   $	��270��6�   $��Arial)�6�   $��24(�6�   $��20,0,0,0q�6�   $
 True��6�   �� 5   
   -   5        $ txtbx_Deadline��5      $��Center=�6*   $
��Left=�6L   $��40��6g   $
 Wrap=�6s   $��Center=�6�   $	��270��6�   $��Arial)�6�   $��24(�6�   $��	310,0,0,0q�6�   $
 True��6�   �� 5   
   $��	Deadline:��6   $��Center=�6$   $
��Left=�6F   $��40��6a   $��309,340,0,0q�6m   $	��Top=�6�   $	��271��6�   $��Arial)�6�   $��24(�6�   �� 5   
   $��Creation Date:��6   $��Center=�6)   $
��Left=�6K   $��40��6f   $��
20,340,0,0q�6r   $	��Top=�6�   $	��270��6�   $��Arial)�6�   $��24(�6�   �� 5   
   $��Task Priority:��6   $��Center=�6)   $
��Left=�6K   $��40��6f   $��
20,440,0,0q�6r   $	��Top=�6�   $	��270��6�   $��Arial)�6�   $��24(�6�   �� 5   
   -   5         ��Name$ btn_Priority1��5      $��1��6(   $
��Left=�64   $��80��6O   $��
28,485,0,0q�6[   $	��Top=�6o   $	��120��6�   ���	#FFBFECFD$��32(�6�   $��Arial)�6�    J� IsHitTestVisible$ False��6�   6�   �� 5   
   -   5        $ btn_Priority2��5      $��2��6(   $
��Left=�64   $��80��6O   $��170,485,0,0q�6[   $	��Top=�6p   $	��120��6�   ���	#FF8CE0FF$��Arial)�6�   $��1,1,1,1q�6�   $��32(�6�   $ False��6�   6�   �� 5   
   -   5        $ btn_Priority3��5      $��3��6(   $
��Left=�64   $��80��6O   $��310,485,0,0q�6[   $	��Top=�6p   $	��120��6�   ���	#FF00BAFF$��32(�6�   $��Arial)�6�   $ False��6�   6�   �� 5   
   -   5        $ btn_Ok��5      $��OK��6!   $
��Left=�6.   $��70��6I   $��
28,690,0,0q�6U   $	��Top=�6i   $	��544��6�   ���	#FFCBF1FF$��24(�6�   $��Arial)�6�   6�   �� 5   
   $;��5Note: Only the leader can change the task parameters.��6   $
��Left=�6P   $��37��6k   $��
28,570,0,0q�6w   $	��Top=�6�   $	��507��6�   $��20(�6�   $��Arial)�6�   �� 5   
   -	   5        $ btn_DeleteTask��5      $��Delete task��6)   $
��Left=�6?   $��70��6Z   $��309,690,0,0q�6f   $	��Top=�6{   $	��262��6�   ���	#FFCBF1FF$��24(�6�   $��Arial)�6�   
���  ��$y�Hidden=�6  5      5      