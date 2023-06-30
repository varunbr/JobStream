import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { JobStreamsComponent } from './job-streams/job-streams.component';
import { JobStreamComponent } from './job-stream/job-stream.component';
import { HistoryComponent } from './history/history.component';
import { RunHistoryComponent } from './run-history/run-history.component';

const routes: Routes = [
  { path: 'jobstream', component: JobStreamsComponent },
  { path: 'jobStream/:id', component: JobStreamComponent },
  { path: 'history', component: HistoryComponent },
  { path: 'history/:id', component: RunHistoryComponent },
  { path: '**', component: HomeComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
