const { app, BrowserWindow, ipcMain} = require('electron')
const path = require('path')
const windowState = require('electron-window-state')
const { electron } = require('process')

// let mainWindow
let clientOne
let clientTwo

function createClient(client) {
    let state = windowState({
        defaultWidth: 800,
        defaultHeight: 600
    })

    client = new BrowserWindow({
        x: state.x,
        y: state.y,
        width: state.width,
        height: state.height,
        minWidth:800,
        minHeight:600,
        webPreferences: {
            preload: path.join(__dirname, 'preload.js'),
            nodeIntegration: true,
            contextIsolation: false,
            // enableRemoteModule: true,
        },
        autoHideMenuBar: true,
        titleBarOverlay: {
            color: '#2f3241',
            symbolColor: '#74b1be',
            height: 20
          },
        backgroundColor: "#0a0a0a",
        shadow: true,
        title: 'Login',
        icon: '/assets/icon/favicon-32x32.png'
    })

    client.loadFile('src/index.html')

    client.webContents.openDevTools();

    client.on('closed',  () => {
        client = null
    })

    state.manage(client)
}

function createWindows() {
    createClient(clientOne)
    createClient(clientTwo)
}

app.on('window-all-closed', () => {
  if (process.platform !== 'darwin') app.quit()
})

app.on('activate', () => {
    if (clientOne === null) createClient(clientOne)
    if (clientTwo === null) createClient(clientTwo)
})

app.commandLine.appendSwitch('ignore-certificate-errors')

app.on('ready', createWindows)

ipcMain.on('login-success', e => { 
    let activeWindow = e.sender.getOwnerBrowserWindow()
    activeWindow.loadFile('src/server/server_select.html')
    activeWindow.title = 'Server Listing'
})

ipcMain.on('redirect', (e, location, title, argObject) => {
    let activeWindow = e.sender.getOwnerBrowserWindow()
    activeWindow.loadFile(location, {query: argObject }) 
    activeWindow.title = title
})

ipcMain.on('logout', e=>{
    let activeWindow = e.sender.getOwnerBrowserWindow()
    activeWindow.loadFile('src/index.html')
    activeWindow.title = 'Login'
})

ipcMain.on('logout-purge', (e, username) => {
    clearSession()
    let activeWindow = e.sender.getOwnerBrowserWindow()
    activeWindow.loadFile('src/index.html')
    activeWindow.title = 'Login'
})

ipcMain.on('error-message', (e, message) => {
    let errorWindow = new BrowserWindow({
        parent: e.sender.getOwnerBrowserWindow(), 
        modal: true, 
        autoHideMenuBar: true, 
        width:600, 
        height:200,
        webPreferences: {
            nodeIntegration: true,
            contextIsolation: false,
        },
        title:'Error'
        })
    
    errorWindow.loadFile("src/error.html", {query: {"message": message }});

    errorWindow.on('close', _=>{
        errorWindow = null;
    })
})

ipcMain.on('client-close-window', e => {
    e.sender.getOwnerBrowserWindow().close()
})

function clearSession(username) {
    window.sessionStorage.removeItem(`${username}_username`);
    window.sessionStorage.removeItem(`${username}_token`);
    window.sessionStorage.removeItem(`${username}_issueDate`);
}


