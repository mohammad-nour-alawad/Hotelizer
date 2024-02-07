var StyleKitName = {};
(function () {

    //// Drawing Methods

    function drawGears(canvas, rotation, targetFrame, resizing) {
        //// General Declarations
        canvas = initializeCanvas(typeof canvas === 'string' ? document.getElementById(canvas) : canvas);
        var context = canvas.getContext('2d');
        var pixelRatio = canvas.paintCodePixelRatio;

        //// Resize to Target Frame
        context.save();
        var resizedFrame = applyResizingBehavior(resizing, makeRect(0, 0, 241, 217), targetFrame);
        context.translate(resizedFrame.x, resizedFrame.y);
        context.scale(resizedFrame.w / 241, resizedFrame.h / 217);


        //// Color Declarations
        var blue = 'rgba(53, 126, 228, 1)';
        var color = 'rgba(53, 126, 228, 1)';
        var color2 = 'rgba(0, 161, 87, 1)';
        var color3 = 'rgba(245, 180, 61, 1)';
        var color4 = 'rgba(235, 57, 49, 1)';

        //// Variable Declarations
        var rotationInverted = -rotation;

        //// Group
        context.save();
        context.translate(74.8, 78.08);
        context.rotate(-rotation * Math.PI / 180);



        //// Rectangle Drawing
        context.save();
        context.translate(-0, 0);

        roundedRect(context, -4.5, -41.5, 10, 20, 5);
        context.fillStyle = color4;
        context.fill();

        context.restore();


        //// Rectangle 2 Drawing
        context.save();
        context.translate(0, -0.01);
        context.rotate(-30 * Math.PI / 180);

        roundedRect(context, -4.5, -41.5, 10, 20, 5);
        context.fillStyle = color4;
        context.fill();

        context.restore();


        //// Rectangle 3 Drawing
        context.save();
        context.translate(0.02, 0.02);
        context.rotate(-60 * Math.PI / 180);

        roundedRect(context, -4.5, -41.5, 10, 20, 5);
        context.fillStyle = color4;
        context.fill();

        context.restore();


        //// Rectangle 4 Drawing
        context.save();
        context.translate(0.04, -0.03);
        context.rotate(-90 * Math.PI / 180);

        roundedRect(context, -4.5, -41.5, 10, 20, 5);
        context.fillStyle = color4;
        context.fill();

        context.restore();


        //// Rectangle 5 Drawing
        context.save();
        context.translate(-0, 0);
        context.rotate(-120 * Math.PI / 180);

        roundedRect(context, -4.5, -41.5, 10, 20, 5);
        context.fillStyle = color4;
        context.fill();

        context.restore();


        //// Rectangle 6 Drawing
        context.save();
        context.translate(0.01, -0.01);
        context.rotate(-150 * Math.PI / 180);

        roundedRect(context, -4.5, -41.5, 10, 20, 5);
        context.fillStyle = color4;
        context.fill();

        context.restore();


        //// Rectangle 8 Drawing
        context.save();
        context.translate(0.02, 0.01);
        context.rotate(-180 * Math.PI / 180);

        roundedRect(context, -4.5, -41.5, 10, 20, 5);
        context.fillStyle = color4;
        context.fill();

        context.restore();


        //// Rectangle 9 Drawing
        context.save();
        context.translate(0.05, -0.01);
        context.rotate(-210 * Math.PI / 180);

        roundedRect(context, -4.5, -41.5, 10, 20, 5);
        context.fillStyle = color4;
        context.fill();

        context.restore();


        //// Rectangle 10 Drawing
        context.save();
        context.translate(0.04, -0.01);
        context.rotate(120 * Math.PI / 180);

        roundedRect(context, -4.5, -41.5, 10, 20, 5);
        context.fillStyle = color4;
        context.fill();

        context.restore();


        //// Rectangle 11 Drawing
        context.save();
        context.translate(0.01, 0.02);
        context.rotate(90 * Math.PI / 180);

        roundedRect(context, -4.5, -41.5, 10, 20, 5);
        context.fillStyle = color4;
        context.fill();

        context.restore();


        //// Rectangle 12 Drawing
        context.save();
        context.translate(0.01, -0.02);
        context.rotate(60 * Math.PI / 180);

        roundedRect(context, -4.5, -41.5, 10, 20, 5);
        context.fillStyle = color4;
        context.fill();

        context.restore();


        //// Rectangle 13 Drawing
        context.save();
        context.translate(0.03, 0.01);
        context.rotate(30 * Math.PI / 180);

        roundedRect(context, -4.5, -41.5, 10, 20, 5);
        context.fillStyle = color4;
        context.fill();

        context.restore();


        //// Bezier Drawing
        context.beginPath();
        context.moveTo(-20.84, -2.63);
        context.bezierCurveTo(-22.18, 8.89, -13.93, 19.31, -2.41, 20.65);
        context.bezierCurveTo(9.11, 21.99, 19.54, 13.74, 20.87, 2.22);
        context.bezierCurveTo(22.21, -9.3, 13.96, -19.73, 2.44, -21.07);
        context.bezierCurveTo(-9.08, -22.41, -19.5, -14.15, -20.84, -2.63);
        context.closePath();
        context.moveTo(4.07, -35.11);
        context.bezierCurveTo(23.27, -32.88, 37.02, -15.5, 34.79, 3.7);
        context.bezierCurveTo(32.56, 22.9, 15.18, 36.65, -4.02, 34.42);
        context.bezierCurveTo(-23.22, 32.19, -36.97, 14.81, -34.74, -4.39);
        context.bezierCurveTo(-32.51, -23.59, -15.13, -37.34, 4.07, -35.11);
        context.closePath();
        context.fillStyle = color4;
        context.fill();


        //// Oval Drawing
        oval(context, -8.74, -8.93, 17.35, 17.35);
        context.fillStyle = color4;
        context.fill();




        context.restore();


        //// Group 2
        context.save();
        context.translate(120.3, 139.08);
        context.rotate(-rotationInverted * Math.PI / 180);



        //// Rectangle 7 Drawing
        context.save();
        context.translate(-0, 0);

        roundedRect(context, -4.5, -41.5, 10, 20, 5);
        context.fillStyle = color3;
        context.fill();

        context.restore();


        //// Rectangle 14 Drawing
        context.save();
        context.translate(0, -0.01);
        context.rotate(-30 * Math.PI / 180);

        roundedRect(context, -4.5, -41.5, 10, 20, 5);
        context.fillStyle = color3;
        context.fill();

        context.restore();


        //// Rectangle 15 Drawing
        context.save();
        context.translate(0.02, 0.02);
        context.rotate(-60 * Math.PI / 180);

        roundedRect(context, -4.5, -41.5, 10, 20, 5);
        context.fillStyle = color3;
        context.fill();

        context.restore();


        //// Rectangle 16 Drawing
        context.save();
        context.translate(0.04, -0.03);
        context.rotate(-90 * Math.PI / 180);

        roundedRect(context, -4.5, -41.5, 10, 20, 5);
        context.fillStyle = color3;
        context.fill();

        context.restore();


        //// Rectangle 17 Drawing
        context.save();
        context.translate(-0, 0);
        context.rotate(-120 * Math.PI / 180);

        roundedRect(context, -4.5, -41.5, 10, 20, 5);
        context.fillStyle = color3;
        context.fill();

        context.restore();


        //// Rectangle 18 Drawing
        context.save();
        context.translate(0.01, -0.01);
        context.rotate(-150 * Math.PI / 180);

        roundedRect(context, -4.5, -41.5, 10, 20, 5);
        context.fillStyle = color3;
        context.fill();

        context.restore();


        //// Rectangle 19 Drawing
        context.save();
        context.translate(0.02, 0.01);
        context.rotate(-180 * Math.PI / 180);

        roundedRect(context, -4.5, -41.5, 10, 20, 5);
        context.fillStyle = color3;
        context.fill();

        context.restore();


        //// Rectangle 20 Drawing
        context.save();
        context.translate(0.05, -0.01);
        context.rotate(-210 * Math.PI / 180);

        roundedRect(context, -4.5, -41.5, 10, 20, 5);
        context.fillStyle = color3;
        context.fill();

        context.restore();


        //// Rectangle 21 Drawing
        context.save();
        context.translate(0.04, -0.01);
        context.rotate(120 * Math.PI / 180);

        roundedRect(context, -4.5, -41.5, 10, 20, 5);
        context.fillStyle = color3;
        context.fill();

        context.restore();


        //// Rectangle 22 Drawing
        context.save();
        context.translate(0.01, 0.02);
        context.rotate(90 * Math.PI / 180);

        roundedRect(context, -4.5, -41.5, 10, 20, 5);
        context.fillStyle = color3;
        context.fill();

        context.restore();


        //// Rectangle 23 Drawing
        context.save();
        context.translate(0.01, -0.02);
        context.rotate(60 * Math.PI / 180);

        roundedRect(context, -4.5, -41.5, 10, 20, 5);
        context.fillStyle = color3;
        context.fill();

        context.restore();


        //// Rectangle 24 Drawing
        context.save();
        context.translate(0.03, 0.01);
        context.rotate(30 * Math.PI / 180);

        roundedRect(context, -4.5, -41.5, 10, 20, 5);
        context.fillStyle = color3;
        context.fill();

        context.restore();


        //// Bezier 2 Drawing
        context.beginPath();
        context.moveTo(-20.84, -2.63);
        context.bezierCurveTo(-22.18, 8.89, -13.93, 19.31, -2.41, 20.65);
        context.bezierCurveTo(9.11, 21.99, 19.54, 13.74, 20.87, 2.22);
        context.bezierCurveTo(22.21, -9.3, 13.96, -19.73, 2.44, -21.07);
        context.bezierCurveTo(-9.08, -22.41, -19.5, -14.15, -20.84, -2.63);
        context.closePath();
        context.moveTo(4.07, -35.11);
        context.bezierCurveTo(23.27, -32.88, 37.02, -15.5, 34.79, 3.7);
        context.bezierCurveTo(32.56, 22.9, 15.18, 36.65, -4.02, 34.42);
        context.bezierCurveTo(-23.22, 32.19, -36.97, 14.81, -34.74, -4.39);
        context.bezierCurveTo(-32.51, -23.59, -15.13, -37.34, 4.07, -35.11);
        context.closePath();
        context.fillStyle = color3;
        context.fill();


        //// Bezier 8 Drawing
        context.beginPath();
        context.moveTo(-1.18, -4.16);
        context.bezierCurveTo(-3.51, -3.5, -4.87, -1.11, -4.22, 1.18);
        context.bezierCurveTo(-3.57, 3.47, -1.16, 4.79, 1.17, 4.13);
        context.bezierCurveTo(3.5, 3.47, 4.87, 1.08, 4.22, -1.21);
        context.bezierCurveTo(3.57, -3.5, 1.15, -4.82, -1.18, -4.16);
        context.closePath();
        context.moveTo(15.7, -0.08);
        context.bezierCurveTo(15.7, 8.48, 8.76, 15.42, 0.2, 15.42);
        context.bezierCurveTo(-8.36, 15.42, -15.3, 8.48, -15.3, -0.08);
        context.bezierCurveTo(-15.3, -8.64, -8.36, -15.58, 0.2, -15.58);
        context.bezierCurveTo(8.76, -15.58, 15.7, -8.64, 15.7, -0.08);
        context.closePath();
        context.fillStyle = color3;
        context.fill();




        context.restore();


        //// Group 3
        context.save();
        context.translate(191.3, 110.58);
        context.rotate(-rotation * Math.PI / 180);



        //// Rectangle 25 Drawing
        context.save();
        context.translate(-0, 0);

        roundedRect(context, -4.5, -41.5, 10, 20, 5);
        context.fillStyle = color2;
        context.fill();

        context.restore();


        //// Rectangle 26 Drawing
        context.save();
        context.translate(0, -0.01);
        context.rotate(-30 * Math.PI / 180);

        roundedRect(context, -4.5, -41.5, 10, 20, 5);
        context.fillStyle = color2;
        context.fill();

        context.restore();


        //// Rectangle 27 Drawing
        context.save();
        context.translate(0.02, 0.02);
        context.rotate(-60 * Math.PI / 180);

        roundedRect(context, -4.5, -41.5, 10, 20, 5);
        context.fillStyle = color2;
        context.fill();

        context.restore();


        //// Rectangle 28 Drawing
        context.save();
        context.translate(0.04, -0.03);
        context.rotate(-90 * Math.PI / 180);

        roundedRect(context, -4.5, -41.5, 10, 20, 5);
        context.fillStyle = color2;
        context.fill();

        context.restore();


        //// Rectangle 29 Drawing
        context.save();
        context.translate(-0, 0);
        context.rotate(-120 * Math.PI / 180);

        roundedRect(context, -4.5, -41.5, 10, 20, 5);
        context.fillStyle = color2;
        context.fill();

        context.restore();


        //// Rectangle 30 Drawing
        context.save();
        context.translate(0.01, -0.01);
        context.rotate(-150 * Math.PI / 180);

        roundedRect(context, -4.5, -41.5, 10, 20, 5);
        context.fillStyle = color2;
        context.fill();

        context.restore();


        //// Rectangle 31 Drawing
        context.save();
        context.translate(0.02, 0.01);
        context.rotate(-180 * Math.PI / 180);

        roundedRect(context, -4.5, -41.5, 10, 20, 5);
        context.fillStyle = color2;
        context.fill();

        context.restore();


        //// Rectangle 32 Drawing
        context.save();
        context.translate(0.05, -0.01);
        context.rotate(-210 * Math.PI / 180);

        roundedRect(context, -4.5, -41.5, 10, 20, 5);
        context.fillStyle = color2;
        context.fill();

        context.restore();


        //// Rectangle 33 Drawing
        context.save();
        context.translate(0.04, -0.01);
        context.rotate(120 * Math.PI / 180);

        roundedRect(context, -4.5, -41.5, 10, 20, 5);
        context.fillStyle = color2;
        context.fill();

        context.restore();


        //// Rectangle 34 Drawing
        context.save();
        context.translate(0.01, 0.02);
        context.rotate(90 * Math.PI / 180);

        roundedRect(context, -4.5, -41.5, 10, 20, 5);
        context.fillStyle = color2;
        context.fill();

        context.restore();


        //// Rectangle 35 Drawing
        context.save();
        context.translate(0.01, -0.02);
        context.rotate(60 * Math.PI / 180);

        roundedRect(context, -4.5, -41.5, 10, 20, 5);
        context.fillStyle = color2;
        context.fill();

        context.restore();


        //// Rectangle 36 Drawing
        context.save();
        context.translate(0.03, 0.01);
        context.rotate(30 * Math.PI / 180);

        roundedRect(context, -4.5, -41.5, 10, 20, 5);
        context.fillStyle = color2;
        context.fill();

        context.restore();


        //// Bezier 3 Drawing
        context.beginPath();
        context.moveTo(-20.84, -2.63);
        context.bezierCurveTo(-22.18, 8.89, -13.93, 19.31, -2.41, 20.65);
        context.bezierCurveTo(9.11, 21.99, 19.54, 13.74, 20.87, 2.22);
        context.bezierCurveTo(22.21, -9.3, 13.96, -19.73, 2.44, -21.07);
        context.bezierCurveTo(-9.08, -22.41, -19.5, -14.15, -20.84, -2.63);
        context.closePath();
        context.moveTo(4.07, -35.11);
        context.bezierCurveTo(23.27, -32.88, 37.02, -15.5, 34.79, 3.7);
        context.bezierCurveTo(32.56, 22.9, 15.18, 36.65, -4.02, 34.42);
        context.bezierCurveTo(-23.22, 32.19, -36.97, 14.81, -34.74, -4.39);
        context.bezierCurveTo(-32.51, -23.59, -15.13, -37.34, 4.07, -35.11);
        context.closePath();
        context.fillStyle = color2;
        context.fill();


        //// Oval 3 Drawing
        context.save();
        context.translate(0.01, 0.01);
        context.rotate(47.74 * Math.PI / 180);

        oval(context, -3.42, -3.42, 6.84, 6.84);
        context.fillStyle = color2;
        context.fill();

        context.restore();




        context.restore();


        //// Group 4
        context.save();
        context.translate(49.3, 167.58);
        context.rotate(-rotation * Math.PI / 180);



        //// Rectangle 37 Drawing
        context.save();
        context.translate(-0, 0);

        roundedRect(context, -4.5, -41.5, 10, 20, 5);
        context.fillStyle = color;
        context.fill();

        context.restore();


        //// Rectangle 38 Drawing
        context.save();
        context.translate(0, -0.01);
        context.rotate(-30 * Math.PI / 180);

        roundedRect(context, -4.5, -41.5, 10, 20, 5);
        context.fillStyle = color;
        context.fill();

        context.restore();


        //// Rectangle 39 Drawing
        context.save();
        context.translate(0.02, 0.02);
        context.rotate(-60 * Math.PI / 180);

        roundedRect(context, -4.5, -41.5, 10, 20, 5);
        context.fillStyle = color;
        context.fill();

        context.restore();


        //// Rectangle 40 Drawing
        context.save();
        context.translate(0.04, -0.03);
        context.rotate(-90 * Math.PI / 180);

        roundedRect(context, -4.5, -41.5, 10, 20, 5);
        context.fillStyle = color;
        context.fill();

        context.restore();


        //// Rectangle 41 Drawing
        context.save();
        context.translate(-0, 0);
        context.rotate(-120 * Math.PI / 180);

        roundedRect(context, -4.5, -41.5, 10, 20, 5);
        context.fillStyle = color;
        context.fill();

        context.restore();


        //// Rectangle 42 Drawing
        context.save();
        context.translate(0.01, -0.01);
        context.rotate(-150 * Math.PI / 180);

        roundedRect(context, -4.5, -41.5, 10, 20, 5);
        context.fillStyle = color;
        context.fill();

        context.restore();


        //// Rectangle 43 Drawing
        context.save();
        context.translate(0.02, 0.01);
        context.rotate(-180 * Math.PI / 180);

        roundedRect(context, -4.5, -41.5, 10, 20, 5);
        context.fillStyle = color;
        context.fill();

        context.restore();


        //// Rectangle 44 Drawing
        context.save();
        context.translate(0.05, -0.01);
        context.rotate(-210 * Math.PI / 180);

        roundedRect(context, -4.5, -41.5, 10, 20, 5);
        context.fillStyle = color;
        context.fill();

        context.restore();


        //// Rectangle 45 Drawing
        context.save();
        context.translate(0.04, -0.01);
        context.rotate(120 * Math.PI / 180);

        roundedRect(context, -4.5, -41.5, 10, 20, 5);
        context.fillStyle = color;
        context.fill();

        context.restore();


        //// Rectangle 46 Drawing
        context.save();
        context.translate(0.01, 0.02);
        context.rotate(90 * Math.PI / 180);

        roundedRect(context, -4.5, -41.5, 10, 20, 5);
        context.fillStyle = color;
        context.fill();

        context.restore();


        //// Rectangle 47 Drawing
        context.save();
        context.translate(0.01, -0.02);
        context.rotate(60 * Math.PI / 180);

        roundedRect(context, -4.5, -41.5, 10, 20, 5);
        context.fillStyle = color;
        context.fill();

        context.restore();


        //// Rectangle 48 Drawing
        context.save();
        context.translate(0.03, 0.01);
        context.rotate(30 * Math.PI / 180);

        roundedRect(context, -4.5, -41.5, 10, 20, 5);
        context.fillStyle = color;
        context.fill();

        context.restore();


        //// Bezier 4 Drawing
        context.beginPath();
        context.moveTo(-20.84, -2.63);
        context.bezierCurveTo(-22.18, 8.89, -13.93, 19.31, -2.41, 20.65);
        context.bezierCurveTo(9.11, 21.99, 19.54, 13.74, 20.87, 2.22);
        context.bezierCurveTo(22.21, -9.3, 13.96, -19.73, 2.44, -21.07);
        context.bezierCurveTo(-9.08, -22.41, -19.5, -14.15, -20.84, -2.63);
        context.closePath();
        context.moveTo(4.07, -35.11);
        context.bezierCurveTo(23.27, -32.88, 37.02, -15.5, 34.79, 3.7);
        context.bezierCurveTo(32.56, 22.9, 15.18, 36.65, -4.02, 34.42);
        context.bezierCurveTo(-23.22, 32.19, -36.97, 14.81, -34.74, -4.39);
        context.bezierCurveTo(-32.51, -23.59, -15.13, -37.34, 4.07, -35.11);
        context.closePath();
        context.fillStyle = color;
        context.fill();


        //// Bezier 7 Drawing
        context.save();
        context.translate(-0.03, 0.03);

        context.beginPath();
        context.moveTo(-4.16, -0.26);
        context.bezierCurveTo(-4.42, 1.9, -2.85, 3.86, -0.66, 4.11);
        context.bezierCurveTo(1.52, 4.36, 3.5, 2.81, 3.76, 0.65);
        context.bezierCurveTo(4.01, -1.51, 2.45, -3.46, 0.26, -3.71);
        context.bezierCurveTo(-1.93, -3.96, -3.91, -2.42, -4.16, -0.26);
        context.closePath();
        context.moveTo(0.57, -6.35);
        context.bezierCurveTo(4.21, -5.93, 6.82, -2.67, 6.4, 0.93);
        context.bezierCurveTo(5.98, 4.53, 2.68, 7.11, -0.97, 6.69);
        context.bezierCurveTo(-4.61, 6.27, -7.22, 3.01, -6.8, -0.59);
        context.bezierCurveTo(-6.38, -4.19, -3.08, -6.76, 0.57, -6.35);
        context.closePath();
        context.fillStyle = color;
        context.fill();

        context.restore();




        context.restore();


        //// Group 5
        context.save();
        context.translate(145.8, 49.58);
        context.rotate(-rotationInverted * Math.PI / 180);



        //// Rectangle 49 Drawing
        context.save();
        context.translate(-0, 0);

        roundedRect(context, -4.5, -41.5, 10, 20, 5);
        context.fillStyle = blue;
        context.fill();

        context.restore();


        //// Rectangle 50 Drawing
        context.save();
        context.translate(0, -0.01);
        context.rotate(-30 * Math.PI / 180);

        roundedRect(context, -4.5, -41.5, 10, 20, 5);
        context.fillStyle = blue;
        context.fill();

        context.restore();


        //// Rectangle 51 Drawing
        context.save();
        context.translate(0.02, 0.02);
        context.rotate(-60 * Math.PI / 180);

        roundedRect(context, -4.5, -41.5, 10, 20, 5);
        context.fillStyle = blue;
        context.fill();

        context.restore();


        //// Rectangle 52 Drawing
        context.save();
        context.translate(0.04, -0.03);
        context.rotate(-90 * Math.PI / 180);

        roundedRect(context, -4.5, -41.5, 10, 20, 5);
        context.fillStyle = blue;
        context.fill();

        context.restore();


        //// Rectangle 53 Drawing
        context.save();
        context.translate(-0, 0);
        context.rotate(-120 * Math.PI / 180);

        roundedRect(context, -4.5, -41.5, 10, 20, 5);
        context.fillStyle = blue;
        context.fill();

        context.restore();


        //// Rectangle 54 Drawing
        context.save();
        context.translate(0.01, -0.01);
        context.rotate(-150 * Math.PI / 180);

        roundedRect(context, -4.5, -41.5, 10, 20, 5);
        context.fillStyle = blue;
        context.fill();

        context.restore();


        //// Rectangle 55 Drawing
        context.save();
        context.translate(0.02, 0.01);
        context.rotate(-180 * Math.PI / 180);

        roundedRect(context, -4.5, -41.5, 10, 20, 5);
        context.fillStyle = blue;
        context.fill();

        context.restore();


        //// Rectangle 56 Drawing
        context.save();
        context.translate(0.05, -0.01);
        context.rotate(-210 * Math.PI / 180);

        roundedRect(context, -4.5, -41.5, 10, 20, 5);
        context.fillStyle = blue;
        context.fill();

        context.restore();


        //// Rectangle 57 Drawing
        context.save();
        context.translate(0.04, -0.01);
        context.rotate(120 * Math.PI / 180);

        roundedRect(context, -4.5, -41.5, 10, 20, 5);
        context.fillStyle = blue;
        context.fill();

        context.restore();


        //// Rectangle 58 Drawing
        context.save();
        context.translate(0.01, 0.02);
        context.rotate(90 * Math.PI / 180);

        roundedRect(context, -4.5, -41.5, 10, 20, 5);
        context.fillStyle = blue;
        context.fill();

        context.restore();


        //// Rectangle 59 Drawing
        context.save();
        context.translate(0.01, -0.02);
        context.rotate(60 * Math.PI / 180);

        roundedRect(context, -4.5, -41.5, 10, 20, 5);
        context.fillStyle = blue;
        context.fill();

        context.restore();


        //// Rectangle 60 Drawing
        context.save();
        context.translate(0.03, 0.01);
        context.rotate(30 * Math.PI / 180);

        roundedRect(context, -4.5, -41.5, 10, 20, 5);
        context.fillStyle = blue;
        context.fill();

        context.restore();


        //// Bezier 5 Drawing
        context.beginPath();
        context.moveTo(-20.84, -2.63);
        context.bezierCurveTo(-22.18, 8.89, -13.93, 19.31, -2.41, 20.65);
        context.bezierCurveTo(9.11, 21.99, 19.54, 13.74, 20.87, 2.22);
        context.bezierCurveTo(22.21, -9.3, 13.96, -19.73, 2.44, -21.07);
        context.bezierCurveTo(-9.08, -22.41, -19.5, -14.15, -20.84, -2.63);
        context.closePath();
        context.moveTo(4.07, -35.11);
        context.bezierCurveTo(23.27, -32.88, 37.02, -15.5, 34.79, 3.7);
        context.bezierCurveTo(32.56, 22.9, 15.18, 36.65, -4.02, 34.42);
        context.bezierCurveTo(-23.22, 32.19, -36.97, 14.81, -34.74, -4.39);
        context.bezierCurveTo(-32.51, -23.59, -15.13, -37.34, 4.07, -35.11);
        context.closePath();
        context.fillStyle = blue;
        context.fill();


        //// Bezier 6 Drawing
        context.beginPath();
        context.moveTo(-7.74, -1.09);
        context.bezierCurveTo(-8.23, 3.18, -5.2, 7.05, -0.99, 7.55);
        context.bezierCurveTo(3.23, 8.05, 7.05, 4.98, 7.54, 0.71);
        context.bezierCurveTo(8.03, -3.57, 5.01, -7.44, 0.79, -7.94);
        context.bezierCurveTo(-3.43, -8.43, -7.24, -5.37, -7.74, -1.09);
        context.closePath();
        context.moveTo(1.39, -13.15);
        context.bezierCurveTo(8.42, -12.32, 13.45, -5.87, 12.64, 1.26);
        context.bezierCurveTo(11.82, 8.38, 5.46, 13.49, -1.57, 12.66);
        context.bezierCurveTo(-8.6, 11.83, -13.64, 5.38, -12.82, -1.74);
        context.bezierCurveTo(-12.01, -8.87, -5.64, -13.98, 1.39, -13.15);
        context.closePath();
        context.fillStyle = blue;
        context.fill();




        context.restore();

        context.restore();

    }

    //// Infrastructure

    function clearCanvas(canvas) {
        canvas = initializeCanvas(typeof canvas === 'string' ? document.getElementById(canvas) : canvas);
        canvas.getContext('2d').clearRect(0, 0, canvas.width, canvas.height);
    }

    // Possible arguments for 'resizing' parameter are:
    //   'aspectfit': The content is proportionally resized to fit into the target rectangle.
    //   'aspectfill': The content is proportionally resized to completely fill the target rectangle.
    //   'stretch': The content is stretched to match the entire target rectangle.
    //   'center': The content is centered in the target rectangle, but it is NOT resized.
    function applyResizingBehavior(resizing, rect, targetRect) {
        if (targetRect === undefined || equalRects(rect, targetRect) || equalRects(targetRect, makeRect(0, 0, 0, 0))) {
            return rect;
        }

        var scales = makeSize(0, 0);
        scales.w = Math.abs(targetRect.w / rect.w);
        scales.h = Math.abs(targetRect.h / rect.h);

        switch (resizing) {
            case 'aspectfit': {
                scales.w = Math.min(scales.w, scales.h);
                scales.h = scales.w;
                break;
            }
            case 'aspectfill': {
                scales.w = Math.max(scales.w, scales.h);
                scales.h = scales.w;
                break;
            }
            case 'stretch':
            case undefined:
                break;
            case 'center': {
                scales.w = 1;
                scales.h = 1;
                break;
            }
            default:
                throw 'Unknown resizing behavior "' + resizing + '". Use "aspectfit", "aspectfill", "stretch" or "center" as resizing behavior.';
        }

        var result = makeRect(Math.min(rect.x, rect.x + rect.w), Math.min(rect.y, rect.y + rect.h), Math.abs(rect.w), Math.abs(rect.h));
        result.w *= scales.w;
        result.h *= scales.h;
        result.x = targetRect.x + (targetRect.w - result.w) / 2;
        result.y = targetRect.y + (targetRect.h - result.h) / 2;
        return result;
    }

    function oval(context, x, y, w, h) {
        context.save();
        context.beginPath();
        context.translate(x, y);
        context.scale(w / 2, h / 2);
        context.arc(1, 1, 1, 0, 2 * Math.PI, false);
        context.closePath();
        context.restore();
    }

    function roundedRect(context, x, y, w, h, r) {
        context.beginPath();
        context.arc(x + r, y + r, r, Math.PI, 1.5 * Math.PI);
        context.arc(x + w - r, y + r, r, 1.5 * Math.PI, 2 * Math.PI);
        context.arc(x + w - r, y + h - r, r, 0, 0.5 * Math.PI);
        context.arc(x + r, y + h - r, r, 0.5 * Math.PI, Math.PI);
        context.closePath();
    }

    function makeRect(x, y, w, h) {
        return { x: x, y: y, w: w, h: h };
    }

    function equalRects(r1, r2) {
        return r1.x === r2.x && r1.y === r2.y && r1.w == r2.w && r1.h === r2.h;
    }

    function makeSize(w, h) {
        return { w: w, h: h };
    }

    function initializeCanvas(canvas) {
        if ('paintCodePixelRatio' in canvas) return canvas;
        // This function should only be called once on each canvas.
        var context = canvas.getContext('2d');

        var devicePixelRatio = window.devicePixelRatio || 1;
        var backingStorePixelRatio = context.webkitBackingStorePixelRatio
            || context.mozBackingStorePixelRatio
            || context.msBackingStorePixelRatio
            || context.oBackingStorePixelRatio
            || context.backingStorePixelRatio
            || 1;

        var pixelRatio = devicePixelRatio / backingStorePixelRatio;

        canvas.style.width = canvas.width + 'px';
        canvas.style.height = canvas.height + 'px';
        canvas.width *= pixelRatio;
        canvas.height *= pixelRatio;
        canvas.paintCodePixelRatio = pixelRatio;

        context.scale(pixelRatio, pixelRatio);
        return canvas;
    }

    //// Public Interface

    // Drawing Methods
    StyleKitName.drawGears = drawGears;

    // Utilities
    StyleKitName.clearCanvas = clearCanvas;
    StyleKitName.makeRect = makeRect;

})();
