import { Component, EventEmitter, Output } from '@angular/core';
import { UserService } from '../user.service';
import { User } from '../user.model';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-user-form',
  templateUrl: './user-form.component.html',
  styleUrls: ['./user-form.component.css']
})
export class UserFormComponent {
  @Output() userCreated = new EventEmitter<void>();

  user: User = {
    userName: '',
    email: '',
    password: '',
    hasConsented: false
  };

  validationErrors: { [key: string]: string[] } = {};

  constructor(private userService: UserService) {}

  createUser(form: NgForm): void {
    this.validationErrors = {};

    this.userService.createUser(this.user).subscribe({
      next: (newUser) => {
        this.user = {
          userName: '',
          email: '',
          password: '',
          hasConsented: false
        };
        form.resetForm();
        this.userCreated.emit();
      },
      error: (error) => {
        if (error.status === 400 && error.error.length) {
          this.processValidationErrors(error.error);
        } else if (error.status === 401 && error.error.detail) {
          this.validationErrors['hasConsented'] = [error.error.detail];
        } else {
          console.error('Server error:', error);
        }
      }
    });
  }

  private processValidationErrors(errors: any[]): void {
    errors.forEach(err => {
      const field = err.propertyName;
      const errorMessage = err.errorMessage;

      if (!this.validationErrors[field]) {
        this.validationErrors[field] = [];
      }
      this.validationErrors[field].push(errorMessage);
    });
  }

  getFieldErrors(field: string): string[] {
    return this.validationErrors[field] || [];
  }
}
