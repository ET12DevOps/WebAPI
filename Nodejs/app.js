let express = require('express')
let app = express()

app.use(express.json())

let alumnos = [
    {id: 1, nombre: 'lucas'},
    {id: 2, nombre: 'nahuel'},
    {id: 3, nombre: 'luciano'}
]

//endpoints
app.get('/', (request, response) => {
    response.send('hola mundo')
})

app.post('/', (request, response) => {
    console.log(request.body)
    response.send('hola mundo')
})    

app.get('/alumno', (req, res) => {
    res.send(alumnos)
})

app.post('/alumno', (req, res) => {
    let alumno = req.body
    alumnos.push(alumno)
    res.send(alumnos)
})


app.listen(3000)
