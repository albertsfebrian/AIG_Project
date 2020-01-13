        private bool splitroom(room newroom)
        {
            if (newroom.width > newroom.height && newroom.width > limit * 2 + 4)
            {
                split width

                to get minimum starting poin + 2(wall)
                int randlimit = newroom.width - limit * 2 + 2;

                int verticalcoord;
                do
                {
                    max - min + 1
                    verticalcoord = rand.next(randlimit) + limit + 2;
                } while (map.coor[newroom.row, newroom.col + verticalcoord - 1].value == map.door || map.coor[newroom.row + newroom.height - 1, newroom.col + verticalcoord - 1].value == map.door);

                int doorrand = rand.next((newroom.height - 1) - 1) + 1;
                map.coor[newroom.row + doorrand, newroom.col + verticalcoord - 1].value = map.door;

                room leftroom = createroom(newroom.col, newroom.row, verticalcoord, newroom.height);
                room rightroom = createroom(newroom.col + verticalcoord - 1, newroom.row, newroom.width - verticalcoord + 1, newroom.height);

                if (leftroom.isleaf)
                    rooms.add(leftroom);
                if (rightroom.isleaf)
                    rooms.add(rightroom);
            }
            else if (newroom.height > newroom.width && newroom.height >= limit * 2 + 4)
            {
                int randlimit = newroom.height - (limit * 2 + 2);
                int horiwall;

                do
                {
                    horiwall = rand.next(randlimit) + limit + 2;
                } while (map.coor[newroom.row + horiwall - 1, newroom.col].value == map.door || map.coor[newroom.row + horiwall - 1, newroom.col + newroom.width - 1].value == map.door);

                int wallrand = rand.next(newroom.width - 2) + 1;
                map.coor[newroom.row + horiwall - 1, newroom.col + wallrand].value = map.door;

                room toproom = createroom(newroom.col, newroom.row, newroom.width, horiwall);
                room bottomroom = createroom(newroom.col, horiwall + newroom.row - 1, newroom.width, newroom.height - horiwall + 1);

                if (toproom.isleaf)
                    rooms.add(toproom);
                if (bottomroom.isleaf)
                    rooms.add(bottomroom);

            }
            else
                return false;

            return true;
        }