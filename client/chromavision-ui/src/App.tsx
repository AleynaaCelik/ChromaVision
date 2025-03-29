import React, { useState } from 'react';
import './App.css';
import { colorService } from './services/api';

function App() {
  const [description, setDescription] = useState('');
  const [palette, setPalette] = useState<any>(null);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState('');

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    
    if (!description) {
      setError('Lütfen bir açıklama girin');
      return;
    }

    setLoading(true);
    setError('');
    
    try {
      const result = await colorService.generateFromText(description);
      setPalette(result.data);
    } catch (err) {
      console.error('Hata:', err);
      setError('Renk paleti oluşturulamadı. Lütfen tekrar deneyin.');
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="App">
      <header className="App-header">
        <h1>ChromaVision Color Palette Generator</h1>
      </header>
      
      <main>
        <form onSubmit={handleSubmit}>
          <div>
            <label htmlFor="description">Renk Paleti Açıklaması:</label>
            <textarea
              id="description"
              value={description}
              onChange={(e) => setDescription(e.target.value)}
              placeholder="Gün batımı renkleri, deniz mavisi tonları, vb."
              rows={3}
            />
          </div>
          
          <button type="submit" disabled={loading}>
            {loading ? 'Oluşturuluyor...' : 'Palet Oluştur'}
          </button>
        </form>
        
        {error && (
          <div className="error">
            {error}
          </div>
        )}
        
        {palette && (
          <div className="palette">
            <h2>{palette.name || 'Oluşturulan Palet'}</h2>
            <div className="color-grid">
              {palette.colors.map((color: string, index: number) => (
                <div
                  key={index}
                  className="color-box"
                  style={{ backgroundColor: color }}
                >
                  <span>{color}</span>
                </div>
              ))}
            </div>
          </div>
        )}
      </main>
    </div>
  );
}

export default App;