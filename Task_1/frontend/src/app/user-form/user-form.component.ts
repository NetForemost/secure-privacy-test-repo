import { Component } from '@angular/core';
import { UserService } from '../user.service';
import { User } from '../user.model';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-user-form',
  templateUrl: './user-form.component.html',
  styleUrls: ['./user-form.component.css']
})
export class UserFormComponent {
  user: User = {
    userName: '',
    email: '',
    password: '',
    hasConsented: false
  };

  constructor(private userService: UserService) { }

  createUser(form: NgForm): void {
    if (form.valid) {
      this.userService.createUser(this.user).subscribe({
        next: (newUser) => {
          console.log('User created:', newUser);
          this.user = {
            userName: '',
            email: '',
            password: '',
            hasConsented: false
          };
          form.resetForm();
        },
        error: (error) => {
          console.error('Error creating user:', error);
        }
      });
    }
  }
}
