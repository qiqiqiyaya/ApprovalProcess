import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-approval-settings',
  templateUrl: './approval-settings.component.html',
  styleUrls: ['./approval-settings.component.css'],
  standalone: false,
})
export class ApprovalSettingsComponent {
  loading = false;
  approverType = 'user';
  selectedUsers: string[] = [];
  selectedRoles: string[] = [];
  leaderLevel = 1;
  approvalMode = 'or';
  autoApprove = false;

  userList = [
    { id: '1', name: '张三' },
    { id: '2', name: '李四' },
    { id: '3', name: '王五' },
    { id: '4', name: '赵六' }
  ];

  roleList = [
    { id: '1', name: '经理' },
    { id: '2', name: '总监' },
    { id: '3', name: '副总' }
  ];

  onApproverTypeChange() {
    this.selectedUsers = [];
    this.selectedRoles = [];
  }

  cancel() {
  }

  confirm() {
  }
}
