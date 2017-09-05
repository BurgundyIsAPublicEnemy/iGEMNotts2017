#include <cairo.h>
#include <gtk/gtk.h>
#include <stdlib.h> 
#include <stdio.h>

//gcc `pkg-config --cflags gtk+-3.0` -o lg  Lucifer_Grapher.c `pkg-config --libs gtk+-3.0` -lX11

static void do_drawing1(cairo_t *, int x, int y);
static void do_drawing_x(cairo_t *, int x);
static void do_drawing_y(cairo_t *cr, int x);

float read_tmp(int x);

static gboolean on_draw_event(GtkWidget *widget, cairo_t *cr, 
    gpointer user_data)
{
int x = 0;
float y = 0.0f;

do {
  do_drawing_x(cr, x);
x = x + 1;
} while (x <= 400);

x = 0;

do {
  do_drawing_y(cr, x);
x = x + 1;
} while (x <= 400);

x = 0;
do {
y = read_tmp(x);
do_drawing1(cr, (y * 10000), x);
x = x + 1;
} while (x <= 1000);
  return FALSE;
}

float read_tmp(int x) {
  float fl;
  int dump;
  char * filename_em[100];
  char buffer_em[64];
  int count = 0;
  // Open file
  FILE * fptr1 = fopen("tmp.txt", "r");
  if (fptr1 == NULL) {
    printf("Cannot open file \n");
    exit(0);
  }
  // Read contents from file
  while (!feof(fptr1)) {
    fgets(buffer_em, 64, fptr1);
    sscanf(buffer_em, "%f %d \n", & fl, & dump);
    if (count == x) {
      printf("%f", fl);
      return fl;
    }
    count++;
  }

  return 0;
}

static void do_drawing_x(cairo_t *cr, int x)
{
  cairo_set_source_rgb(cr, 0, 0, 0);
//[right][down][size]
  cairo_rectangle(cr, 100 + x , 505, 2, 2);
  cairo_set_line_width(cr, 0.4);
  cairo_set_line_join(cr, CAIRO_LINE_JOIN_MITER); 
cairo_fill (cr);
  cairo_stroke(cr);
}

static void do_drawing_y(cairo_t *cr, int x)
{
  cairo_set_source_rgb(cr, 0, 0, 0);
//[right][down][size]
  cairo_rectangle(cr, 100 , 505 - x, 2, 2);
  cairo_set_line_width(cr, 0.4);
  cairo_set_line_join(cr, CAIRO_LINE_JOIN_MITER); 
  cairo_fill (cr);
  cairo_stroke(cr);
}

static void do_drawing1(cairo_t *cr, int x, int y)
{
  cairo_set_source_rgb(cr, 0, 1, 0);
//[right][down][size]
  cairo_rectangle(cr, ((y * 3) + 100 ) , (500 -x), 10, 10);
  cairo_set_line_width(cr, 1);
  cairo_set_line_join(cr, CAIRO_LINE_JOIN_MITER); 
cairo_fill (cr);
  cairo_stroke(cr);
}


int main(int argc, char *argv[])
{
  GtkWidget *window;
  GtkWidget *darea;
  gtk_init(&argc, &argv);

  window = gtk_window_new(GTK_WINDOW_TOPLEVEL);

  darea = gtk_drawing_area_new();
  gtk_container_add(GTK_CONTAINER(window), darea);
 
  gtk_widget_add_events(window, GDK_BUTTON_PRESS_MASK);

  g_signal_connect(G_OBJECT(darea), "draw", 
      G_CALLBACK(on_draw_event), NULL); 
  g_signal_connect(window, "destroy",
      G_CALLBACK(gtk_main_quit), NULL);  

 
  gtk_window_set_position(GTK_WINDOW(window), GTK_WIN_POS_CENTER);
  gtk_window_set_default_size(GTK_WINDOW(window), 600, 600); 
  gtk_window_set_title(GTK_WINDOW(window), "Lucifer Graphing");

  gtk_widget_show_all(window);

  gtk_main();

  return 0;
}
