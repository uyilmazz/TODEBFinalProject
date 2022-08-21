import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CreateMessage } from '../models/createMessage';
import { DataResponseModel } from '../models/dataResponseModel';
import { Message } from '../models/message';
import { ResponseModel } from '../models/responseModel';

@Injectable({
  providedIn: 'root'
})
export class MessageService {

  constructor(private httpClient: HttpClient) { }
  apiUrl: string = "https://localhost:44355/api";


  getMessage(messageId: number): Observable<DataResponseModel<Message>> {
    return this.httpClient.get<DataResponseModel<Message>>(this.apiUrl + "/Messages/id?id=" + messageId);
  }

  getMessages(): Observable<DataResponseModel<Message[]>> {
    return this.httpClient.get<DataResponseModel<Message[]>>(this.apiUrl + "/Messages");
  }

  newMessages(): Observable<DataResponseModel<Message[]>> {
    return this.httpClient.get<DataResponseModel<Message[]>>(this.apiUrl + "/Messages/new");
  }

  readedMessages(): Observable<DataResponseModel<Message[]>> {
    return this.httpClient.get<DataResponseModel<Message[]>>(this.apiUrl + "/Messages/readed");
  }

  unReadedMessages(): Observable<DataResponseModel<Message[]>> {
    return this.httpClient.get<DataResponseModel<Message[]>>(this.apiUrl + "/Messages/unReaded");
  }

  getMessagesBySenderId(senderId: number): Observable<DataResponseModel<Message[]>> {
    return this.httpClient.get<DataResponseModel<Message[]>>(this.apiUrl + "/Messages/senderId?id=" + senderId);
  }

  sendMessage(message: CreateMessage): Observable<ResponseModel> {
    return this.httpClient.post<ResponseModel>(this.apiUrl + "/Messages", message);
  }
}
