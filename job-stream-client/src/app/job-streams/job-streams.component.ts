import { Component, OnInit } from '@angular/core';
import { JobStreamService } from '../services/job-stream.service';
import { Title } from '@angular/platform-browser';
import { Router } from '@angular/router';

export interface PeriodicElement {
  name: string;
  position: number;
  weight: number;
  symbol: string;
}

@Component({
  selector: 'app-job-streams',
  templateUrl: './job-streams.component.html',
  styleUrls: ['./job-streams.component.css'],
})
export class JobStreamsComponent implements OnInit {
  jobStreams: any[] = [];
  constructor(
    private jobStreamService: JobStreamService,
    title: Title,
    private router: Router
  ) {
    title.setTitle('JobStreams');
  }

  displayedColumns: string[] = ['id', 'name', 'updated'];

  ngOnInit(): void {
    this.jobStreamService.getJobStreams().subscribe((response) => {
      this.jobStreams = response;
      console.log(response);
    });
  }

  addToQueue(id: number) {
    this.jobStreamService.addToQueue(id).subscribe((response) => {
      this.router.navigate(['/history/' + response.id]);
    });
  }
}
