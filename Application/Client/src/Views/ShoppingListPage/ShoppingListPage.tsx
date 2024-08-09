import React, { useState, useEffect } from "react";
import { NoteDto, CreateNoteDto, UpdateNoteDto } from "../../Models/notes";
import agent from "../../api/agent";
import {
  PageContainer,
  Title,
  Input,
  Button,
  List,
  ListItem,
} from "./ShoppingListPage.style";

const ShoppingListPage: React.FC = () => {
  const [notes, setNotes] = useState<NoteDto[]>([]);
  const [newNote, setNewNote] = useState<string>("");

  useEffect(() => {
    const fetchNotes = async () => {
      try {
        const fetchedNotes = await agent.Notes.list();
        setNotes(fetchedNotes);
      } catch (error) {
        console.error("Error fetching notes:", error);
      }
    };
    fetchNotes();
  }, []);

  const handleAddNote = async () => {
    const trimmedNote = newNote.trim();
    if (trimmedNote !== "") {
      try {
        const createNoteDto: CreateNoteDto = {
          text: trimmedNote,
          status: false,
        };
        const createdNote = await agent.Notes.add(createNoteDto);
        setNotes([...notes, createdNote]);
        setNewNote("");
      } catch (error) {
        console.error("Error adding note:", error);
      }
    }
  };

  const handleToggleNote = async (noteId: number) => {
    try {
      const noteToUpdate = notes.find((note) => note.id === noteId);
      console.log(noteToUpdate);
      if (noteToUpdate) {
        const updatedStatus = !noteToUpdate.status;
        const updateNoteDto: UpdateNoteDto = {
          text: noteToUpdate.text,
          status: updatedStatus,
        };

        const updatedNote = await agent.Notes.edit(noteId, updateNoteDto);

        if (updatedNote) {
          const updatedNotes = notes.map((note) =>
            note.id === noteId ? updatedNote : note
          );
          setNotes(updatedNotes);
        } else {
          console.error("Error updating note:", updatedNote);
        }
      }
    } catch (error) {
      console.error("Error toggling note:", error);
    }
  };

  const handleDeleteNote = async (noteId: number) => {
    try {
      await agent.Notes.delete(noteId);
      setNotes(notes.filter((note) => note.id !== noteId));
    } catch (error) {
      console.error("Error deleting note:", error);
    }
  };

  return (
    <PageContainer>
      <Title>Shopping List</Title>
      <div>
        <Input
          type="text"
          value={newNote}
          onChange={(e) => setNewNote(e.target.value)}
          placeholder="Add a new item..."
        />
        <Button onClick={handleAddNote}>Add</Button>
      </div>
      <h2>Uncompleted Items</h2>
      <List>
        {notes
          .filter((note) => !note.status)
          .map((note) => (
            <ListItem key={note.id}>
              <input
                type="checkbox"
                checked={note.status}
                onChange={() => handleToggleNote(note.id)}
              />
              <span
                style={{
                  textDecoration: note.status ? "line-through" : "none",
                }}
              >
                {note.text}
              </span>
              <Button onClick={() => handleDeleteNote(note.id)}>Delete</Button>
            </ListItem>
          ))}
      </List>
      <h2>Completed Items</h2>
      <List>
        {notes
          .filter((note) => note.status)
          .map((note) => (
            <ListItem key={note.id}>
              <input
                type="checkbox"
                checked={note.status}
                onChange={() => handleToggleNote(note.id)}
              />
              <span
                style={{
                  textDecoration: note.status ? "line-through" : "none",
                }}
              >
                {note.text}
              </span>
              <Button onClick={() => handleDeleteNote(note.id)}>Delete</Button>
            </ListItem>
          ))}
      </List>
    </PageContainer>
  );
};

export default ShoppingListPage;
