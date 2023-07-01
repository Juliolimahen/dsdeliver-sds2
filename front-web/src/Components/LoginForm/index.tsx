import React, { useState, ChangeEvent, FormEvent } from 'react';
import { Container, FormContainer, Titulo, InputContainer, Input, Button } from './style';

interface LoginFormProps {
    onSubmit: (username: string, password: string) => void;
}


const LoginForm: React.FC<LoginFormProps> = ({ onSubmit }) => {
    const [username, setUsername] = useState<string>('');
    const [password, setPassword] = useState<string>('');

    const handleUsernameChange = (e: ChangeEvent<HTMLInputElement>) => {
        setUsername(e.target.value);
    };

    const handlePasswordChange = (e: ChangeEvent<HTMLInputElement>) => {
        setPassword(e.target.value);
    };

    const handleFormSubmit = (e: FormEvent) => {
        e.preventDefault();

        onSubmit(username, password);
    };

    return (
        <Container>
            <FormContainer onSubmit={handleFormSubmit}>
                <Titulo>Identificação</Titulo>
                <InputContainer>
                    <Input type="text" id="username" value={username} onChange={handleUsernameChange} placeholder="Usuário" />
                </InputContainer>
                <InputContainer>
                    <Input type="password" id="password" value={password} onChange={handlePasswordChange} placeholder="Senha" />
                </InputContainer>
                <Button type="submit">Logar</Button>
            </FormContainer>
        </Container>
    );
};

export default LoginForm;
