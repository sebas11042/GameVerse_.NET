import React, { useState } from 'react';

const CreateCategory = () => {
  const [formData, setFormData] = useState({ name: '' });
  const [success, setSuccess] = useState(false);

  const handleChange = (e) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    // Simulate successful submission
    setSuccess(true);
  };

  return (
    <div className="container">
      <h2>Crear Categoría</h2>
      {success && <div className="alert alert-success">¡Registrado con ÉXITO!</div>}
      <form onSubmit={handleSubmit}>
        <div className="form-group">
          <label htmlFor="name">Nombre de la Categoría</label>
          <input
            type="text"
            className="form-control"
            id="name"
            name="name"
            value={formData.name}
            onChange={handleChange}
            placeholder="Ingrese el nombre de la categoría"
            required
          />
        </div>
        <button type="submit" className="btn btn-primary">Registrar</button>
      </form>
    </div>
  );
};

export default CreateCategory;