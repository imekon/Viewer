group = 5
number = 1
width = 10
height = 10
depth = 10

CreateDrawing(group, number, 
    { 
        {
            {           0,            0, depth }, 
            {       width,            0, depth }, 
            {       width,       height, depth },
            { width * 0.5, height * 1.2, depth },
            {           0,       height, depth }
        },
        {
            {           0,       height, 0 },
            { width * 0.5, height * 1.2, 0 },
            {       width,       height, 0 },
            {       width,            0, 0 }, 
            {           0,            0, 0 } 
        },
        {
            {           0,            0,     0 },
            {       width,            0,     0 },
            {       width,            0, depth },
            {           0,            0, depth }
        }
    })
