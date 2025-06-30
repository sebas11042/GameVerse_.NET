import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Register from './pages/Register';
import Login from './pages/Login';
import Home from './pages/Home';
import Wishlist from './pages/Wishlist';
import Library from './pages/Library';
import Navbar from './components/Navbar';
import Footer from './components/Footer';
import NewGame from './pages/NewGame';

function App() {
    return (
        <Router>
            <Navbar />
            <Routes>
                <Route path="/" element={<Login />} />
                <Route path="/register" element={<Register />} />
                <Route path="/home" element={<Home />} />
                <Route path="/wishlist" element={<Wishlist />} />
                <Route path="/library" element={<Library />} />
                <Route path="/home" element={<Home />} />
                <Route path="/NewGame" element={<NewGame />} />
            </Routes>
            <Footer />
        </Router>
    );
}

export default App;
