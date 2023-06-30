import { Component, OnInit } from '@angular/core';
import { JobStreamService } from '../services/job-stream.service';

@Component({
  selector: 'app-history',
  templateUrl: './history.component.html',
  styleUrls: ['./history.component.css'],
})
export class HistoryComponent implements OnInit {
  public jobStreamHistory: any[] = [];
  constructor(private jobStreamService: JobStreamService) {}

  displayedColumns: string[] = [
    'id',
    'name',
    'added',
    'started',
    'finished',
    'status',
  ];

  ngOnInit(): void {
    this.jobStreamService.getJobStreamHistoryies().subscribe((response) => {
      this.jobStreamHistory = response;
      console.log(response);
    });
  }
}
