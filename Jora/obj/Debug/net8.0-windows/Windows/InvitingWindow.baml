   M S B A M L   `   `   `  ����?  ;Jora, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:JoraJora  5   	       Jora.InvitingWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   $localclr-namespace:Jora     5   	     � Title$  InvitingWindow��5   	   $	��400��6    $	��600��6-   .��5	      � $	��600��6   $	��400��6   .+�5
   
   �� -   5         ��Name$ 	lbl_Login��5
      $5��/Enter the login of the user you want to invite:��6#   $
��Left=�6]   $��40��6x   $��	10,10,0,0q�6�   $	��Top=�6�   $	��500��6�   $��24(�6�   $��Arial)�6�   �� 5   
   -   5         ��Name$ 
btn_Invite��5      $��Invite user��6%   $
��Left=�6;   $��70��6V   $��
20,288,0,0q�6b   $	��Top=�6v   $	��262��6�   ���	#FFCBF1FF$��24(�6�   $��Arial)�6�   6�   �� 5   
   -   5         ��Name$ 	txt_login��5      $��Center=�6%   $��40��6B   $��0,55,0,0q�6N    �� TextWrapping$
 Wrap=�6`   $	��Top=�6t   $	��580��6�   $��24(�6�   $��Arial)�6�   �� 5   
   -   5        $ 
btn_Cancel��5      $��Cancel��6%   $
��Left=�66   $��70��6Q   $��320,288,0,0q�6]   $	��Top=�6r   $	��262��6�   ���	#FFCBF1FF$��24(�6�   $��Arial)�6�   6�   �� 5   
   -   5        $ 	lbl_Error��5      $#��No user with that name found.��6#   $��Center=�6K   $��40��6h   $��	0,243,0,0q�6t   $	��Top=�6�   $	��572��6�   
���  ��$��22(�6�   $��Arial)�6�   $y�Hidden=�6�   �� 5   
   -   5        $ 
btn_Member��5      $��Member��6%   $
��Left=�66   $��70��6Q   $��
14,150,0,0q�6]   $	��Top=�6q   $	��130��6�   ���	#FFCBF1FF$��24(�6�   $��Arial)�6�   6�   �� 5   
   -   5        $ 	btn_Guest��5      $��Guest��6$   $
��Left=�64   $��70��6O   $��160,150,0,0q�6[   $	��Top=�6p   $	��130��6�   ���	#FFCBF1FF$��24(�6�   $��Arial)�6�    J� RenderTransformOrigin$ 0.389,0.499&�6�   6�   �� 5   
   $��Select a user role:��6   $��Center=�6.   $��40��6K   $��	0,110,0,0q�6W   $	��Top=�6j   $	��572��6�   
���   �$��22(�6�   $��Arial)�6�   5      5      