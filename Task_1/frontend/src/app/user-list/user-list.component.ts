import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { UserService } from '../user.service';
import { User } from '../user.model';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent implements OnInit {
  users: User[] = [];
  errorMessage: string = '';
  @Output() userSelected = new EventEmitter<User>();

  constructor(private userService: UserService) {}

  ngOnInit(): void {
    this.loadUsers();
  }

  loadUsers(): void {
    this.userService.getUsers().subscribe({
      next: (users) => {
        this.users = users;
        this.errorMessage = '';
      },
      error: (error) => {
        console.error('Error loading users:', error);
        this.errorMessage = 'An error occurred while loading users.';
      }
    });
  }

  deleteUser(userId: string): void {
    this.userService.deleteUser(userId).subscribe({
      next: () => {
        this.loadUsers();
      },
      error: (error) => {
        console.error('Error deleting user:', error);
        this.errorMessage = 'An error occurred while deleting the user.';
      }
    });
  }

  selectUser(user: User): void {
    this.userSelected.emit(user);
  }

  onUserCreated(): void {
    this.loadUsers();
  }
}
