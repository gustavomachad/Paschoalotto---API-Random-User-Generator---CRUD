const API_URL = "/api/RandomUser"; // relativo ao host/porta da sua API

// Carregar usuários
async function carregarUsuarios() {
    try {
        const resposta = await fetch(`${API_URL}/all`);
        if (!resposta.ok) throw new Error("Erro ao buscar usuários");
        const usuarios = await resposta.json();

        const tabela = document.querySelector("#usuarios tbody");
        tabela.innerHTML = "";

        usuarios.forEach(user => {
            const linha = document.createElement("tr");
            linha.innerHTML = `
        <td><input value="${escapeHtml(user.firstName ?? '')}" onchange="editarCampo('${user.id}', 'firstName', this.value)"></td>
        <td><input value="${escapeHtml(user.email ?? '')}" onchange="editarCampo('${user.id}', 'email', this.value)"></td>
        <td><input value="${escapeHtml(user.phone ?? '')}" onchange="editarCampo('${user.id}', 'phone', this.value)"></td>
        <td><input value="${escapeHtml(user.country ?? '')}" onchange="editarCampo('${user.id}', 'country', this.value)"></td>
        <td>
          <button onclick="excluirUsuario('${user.id}')">Excluir</button>
        </td>
      `;
            tabela.appendChild(linha);
        });
    } catch (err) {
        alert(err.message);
    }
}

// Criar usuário
document.querySelector("#formCriar").addEventListener("submit", async e => {
    e.preventDefault();

    const novoUsuario = {
        firstName: document.querySelector("#firstName").value || "",
        lastName: document.querySelector("#lastName").value || "",
        email: document.querySelector("#email").value || "",
        phone: document.querySelector("#phone").value || "",
        country: document.querySelector("#country").value || ""
    };

    const resp = await fetch(API_URL, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(novoUsuario)
    });

    if (!resp.ok) {
        const txt = await resp.text();
        alert("Erro ao criar usuário: " + txt);
        return;
    }

    // limpa form e recarrega
    e.target.reset();
    carregarUsuarios();
});

// Editar campo direto na tabela
async function editarCampo(id, campo, valor) {
    try {
        const resposta = await fetch(`${API_URL}/byId/${id}`);
        if (!resposta.ok) throw new Error("Usuário não encontrado");
        const usuario = await resposta.json();

        usuario[campo] = valor;

        const resp = await fetch(`${API_URL}/${id}`, {
            method: "PUT",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(usuario)
        });

        if (!resp.ok) {
            const txt = await resp.text();
            alert("Erro ao atualizar: " + txt);
            return;
        }

        carregarUsuarios();
    } catch (err) {
        alert(err.message);
    }
}

// Excluir usuário
async function excluirUsuario(id) {
    if (!confirm("Tem certeza que deseja excluir?")) return;

    const resp = await fetch(`${API_URL}/${id}`, { method: "DELETE" });
    if (!resp.ok) {
        const txt = await resp.text();
        alert("Erro ao excluir: " + txt);
        return;
    }

    carregarUsuarios();
}

// Carregar ao abrir a página
document.addEventListener("DOMContentLoaded", carregarUsuarios);

// pequena função para escapar HTML em valores (segurança básica)
function escapeHtml(str) {
    return String(str).replace(/[&<>"']/g, m => ({ '&': '&amp;', '<': '&lt;', '>': '&gt;', '"': '&quot;', "'": "&#39;" }[m]));
}
