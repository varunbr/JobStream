import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class JobStreamService {
  constructor(private http: HttpClient) {}

  getJobStreams() {
    return this.http.get<any[]>(environment.apiUrl + 'JobProcess');
  }

  getJobStreamHistoryies() {
    return this.http.get<any[]>(environment.apiUrl + 'JobProcess/history');
  }

  getJobStreamHistory(id: number) {
    return this.http.get<any[]>(
      environment.apiUrl + 'JobProcess/history/' + id
    );
  }

  getJobStream(id: number) {
    return this.http.get<any[]>(environment.apiUrl + 'JobProcess/' + id);
  }

  addToQueue(id: number) {
    return this.http.post<any>(
      environment.apiUrl + 'JobProcess/addToQueue/' + id,
      {}
    );
  }
}
