const API_BASE = window.apiBaseUrl;
export const UserService = {
    getAll: async () => {
        return await fetch(`${API_BASE}/user/GetAll`, {
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        })
    },
    create: async (user) => {
        return await fetch(`${API_BASE}/user/create`, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(user)
        });
    },
    update: async (user) => {
        return await fetch(`${API_BASE}/user/update`, {
            method: 'PUT',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(user)
        });
    },
    delete: async (id) => {
        return await fetch(`${API_BASE}/user/delete/${id}`, {
            method: 'DELETE',
            headers: { 'Content-Type': 'application/json' }
        });
    }
};