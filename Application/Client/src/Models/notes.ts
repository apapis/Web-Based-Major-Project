export interface NoteDto {
  id: number;
  text: string;
  status: boolean;
}

export interface CreateNoteDto {
  text: string;
  status: boolean;
}

export interface UpdateNoteDto {
  text: string;
  status: boolean;
}
