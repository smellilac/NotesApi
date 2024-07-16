import React, { FC, ReactElement, useRef, useEffect, useState } from 'react';
import { CreateNoteDto, Client, NoteLookUpDto } from '../api/api';
import Form from 'react-bootstrap/Form';

const apiClient = new Client('https://localhost:44351');

async function createNote(note: CreateNoteDto) {
    await apiClient.create('1.0', note);
    console.log('Note is created.');
}

const NoteList: FC = (): ReactElement => {
    const textInput = useRef<HTMLInputElement | null>(null);
    const [notes, setNotes] = useState<NoteLookUpDto[] | undefined>(undefined);

    async function getNotes() {
        const noteListVm = await apiClient.getAll('1.0');
        setNotes(noteListVm.notes);
    }

    useEffect(() => {
        getNotes();
    }, []);

    const handleKeyPress = (event: React.KeyboardEvent<HTMLInputElement>) => {
        if (event.key === 'Enter') {
            const note: CreateNoteDto = {
                title: event.currentTarget.value,
            };
            createNote(note);
            event.currentTarget.value = '';
            setTimeout(getNotes, 500);
        }
    };

    return (
        <div>
            Notes
            <div>
                <Form.Control
                    ref={textInput}
                    onKeyPress={handleKeyPress}
                    type="text"
                    placeholder="Enter a note"
                />
            </div>
            <section>
                {notes?.map((note) => (
                    <div key={note.id}>{note.title}</div>
                ))}
            </section>
        </div>
    );
};

export default NoteList;
