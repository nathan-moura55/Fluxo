import React, { useState } from 'react';
import { Header } from './Header';
import api from '../services/api'; 

interface LoginProps {
  onEntrar: () => void;
}

export const Login = ({ onEntrar }: LoginProps) => {
  const [email, setEmail] = useState('');
  const [senha, setSenha] = useState('');
  const [carregando, setCarregando] = useState(false);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setCarregando(true);

    try {
      const response = await api.post('/Usuario/login', {
        email: email,
        senha: senha
      });

      const { token } = response.data;

      if (token) {
        localStorage.setItem('token', token);
        onEntrar(); 
      }
    } catch (err: any) {
      alert(err.response?.data?.mensagem || "ERRO AO ENTRAR NO SISTEMA");
    } finally {
      setCarregando(false);
    }
  };

  const containerStyle: React.CSSProperties = {
    height: '80vh',
    display: 'flex',
    justifyContent: 'center',
    alignItems: 'center',
    backgroundColor: '#ffffff',
    fontFamily: 'Inter, sans-serif'
  };

  const cardStyle: React.CSSProperties = {
    width: '100%',
    maxWidth: '380px',
    padding: '40px',
    textAlign: 'center',
    boxShadow: '0px 10px 30px rgba(0, 0, 0, 0.1)'
  };

  const inputStyle: React.CSSProperties = {
    backgroundColor: '#f5f5f5',
    border: '1px solid #e0e0e0',
    padding: '15px 25px',
    borderRadius: '50px',
    outline: 'none',
    width: '100%',
    marginBottom: '15px',
    fontSize: '1rem'
  };

  const buttonStyle: React.CSSProperties = {
    backgroundColor: '#000',
    color: '#fff',
    width: '100%',
    padding: '15px',
    borderRadius: '50px',
    border: 'none',
    cursor: carregando ? 'not-allowed' : 'pointer',
    fontWeight: '600',
    marginTop: '10px',
    letterSpacing: '1px',
    opacity: carregando ? 0.7 : 1
  };

  return (
    <div>
      <Header />
      <div style={containerStyle}>
        <div style={cardStyle}>
          <h1 style={{ textTransform: 'uppercase', letterSpacing: '6px', marginBottom: '40px', fontWeight: '800' }}>
            FLUXO
          </h1>
          
          <form onSubmit={handleSubmit}>
            <input 
              type="email" 
              placeholder="E-MAIL" 
              style={inputStyle} 
              value={email}
              onChange={e => setEmail(e.target.value)}
              required
            />
            <input 
              type="password" 
              placeholder="SENHA" 
              style={inputStyle} 
              value={senha}
              onChange={e => setSenha(e.target.value)}
              required
            />
            
            <button type="submit" style={buttonStyle} disabled={carregando}>
              {carregando ? 'PROCESSANDO...' : 'ENTRAR NO SISTEMA'}
            </button>
          </form>
          
          <p style={{ marginTop: '25px', fontSize: '0.8rem', color: '#666', textTransform: 'uppercase' }}>
            Não possui conta?
          </p>
          <p style={{ marginTop: '10px', fontSize: '0.8rem', color: '#666', textTransform: 'uppercase' }}>
            Gerenciamento de Estoque v1.0
          </p>
        </div>
      </div>
    </div>
  );
};